using UnityEngine;
using System.Collections;

public class UserControl : MonoBehaviour
{
    public float strafeSpeed, walkSpeed, yawSpeed;
    public float maxWingsOpenDelta, baseWingsDrag, maxStallDelta, maxGravityEffect;
    public float jetpackPush, jetpackPushWings, maxVelocity, stallWarningVelocity, stallCriticalVelocity;
    public float pitchSensitivity, wingsMomentumTransferFactor;

    public float maxCameraSpeed;

    public Transform gunpoint, player, testArrow;
    public GameObject jetpack, wings;
    public HUDManager HUDmanager;

    public AudioSource wingsOpenSnd, wingsCloseSnd, stallWarning;

    PlayerData playerData; // Contains data of currently worn armor
    Rigidbody rb;
    float armorSpeedFactor, currentJetpackPush, baseDrag, yaw, initCamEuler_x;
    bool wingsAreOpen, stalling, openingWings, closingWings;

    Quaternion camBaseRotation;
    Vector3 camBasePosition;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerData = GetComponent<PlayerData>();

        wingsAreOpen = false;
        stalling = false;
        openingWings = false;
        closingWings = false;

        currentJetpackPush = jetpackPush;

        baseDrag = rb.drag;
    }

    void Start()
    {
        armorSpeedFactor = playerData.getSpeedFactor();

        HUDmanager.setHitPoints(playerData.getHP());
        HUDmanager.setArmorImage(playerData.getArmorImage());

        HUDmanager.setWingsState(wingsAreOpen);
        HUDmanager.setJumpStats(playerData.getMaxJumpPoints(), playerData.getJumpRechargeTime());
    }

    // Watch out: "interpolate" option in rigidbody will create a bizzare movement in standard walk
    //void FixedUpdate()
    //{
    //    Quaternion deltaRotation = Quaternion.Euler(player.up * Time.deltaTime * wingsMomentumTransferFactor);
    //    rb.MoveRotation(rb.rotation * deltaRotation);
    //}

    void Update ()
    {
        testArrow.rotation = Quaternion.LookRotation(rb.velocity, transform.up);
        

        if (openingWings)
        {
            player.localRotation = Quaternion.Euler(270, 0, 0); // Smooth this
            openingWings = false;
        }
        else if (closingWings)
        {
            // Smooth this
            player.localRotation = Quaternion.Euler(0, 0, 0); 
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            closingWings = false;
        }

        // Main movement
        if (wingsAreOpen) // Fly
        {
            stalling = false;

            if (rb.velocity.magnitude < stallWarningVelocity)
            {
                if (!stallWarning.isPlaying)
                    stallWarning.Play();

                if (rb.velocity.magnitude < stallCriticalVelocity)
                {
                    player.localRotation = Quaternion.RotateTowards(player.localRotation, Quaternion.Euler(180, player.localRotation.y, player.localRotation.z), maxStallDelta);
                    stalling = true;
                }
            }

            // YAW
            // Apply spherical jacobian?
            //yaw += Input.GetAxis("x") * yawSpeed;
            //yaw = Mathf.Clamp(yaw, -1,1);
            //player.transform.localRotation = Quaternion.LookRotation(new Vector3(yaw, 1 - Mathf.Abs(yaw), 0), -Vector3.forward);

            // set a horizontal velocity proportionnal to yaw

            // PITCH
            // Change dummy angle
            float pitchChange = Input.GetAxis("z") * pitchSensitivity;
            player.rotation = Quaternion.RotateTowards(player.rotation, Quaternion.LookRotation(-player.up, player.forward), pitchChange); // minus delta may be applied

            // HERE - SEE FIXEDUPDATE
            // set a vertical acceleration proportional to pitch
            //rb.AddForce(-Vector3.up * Vector3.Dot(Physics.gravity, player.up) * wingsMomentumTransferFactor);

            // transfer momentum from horiz to verti proportional according to heading
            //rb.MoveRotation(rb.rotation * Quaternion.LookRotation(player.up * Time.deltaTime * wingsMomentumTransferFactor));

            if (Quaternion.Angle(Quaternion.LookRotation(rb.velocity), Quaternion.LookRotation(player.up)) > 1.0f)
            {
                //rb.velocity = Vector3.MoveTowards(rb.velocity, player.up, wingsMomentumTransferFactor);
                //rb.velocity = Quaternion.RotateTowards(Quaternion.LookRotation(rb.velocity), Quaternion.LookRotation(player.up), wingsMomentumTransferFactor) * rb.velocity;
            }

        }
        else // Walk
        {
            float x_mvt = Input.GetAxis("x") * strafeSpeed * armorSpeedFactor; // positive = strafe left
            float z_mvt = Input.GetAxis("z") * walkSpeed * armorSpeedFactor; // positive = walk backward

            transform.Translate(x_mvt, 0, z_mvt);

            transform.Rotate(0, Input.GetAxis("Mouse X"), 0); // move mouse horizontally

            Camera.main.transform.RotateAround(transform.position, transform.right, Input.GetAxis("Mouse Y")); // move mouse vertically
            gunpoint.transform.RotateAround(transform.position, transform.right, Input.GetAxis("Mouse Y")); // move mouse vertically
        }

        // Jump (jetpack)
        if (HUDmanager.isJumping())
        {
            if (wingsAreOpen)
            {
                rb.AddForce(player.up * currentJetpackPush);
            }
            else
            {
                rb.AddForce(0, currentJetpackPush, 0);
            }

            jetpack.SetActive(true);
        }
        else
        {
            jetpack.SetActive(false);
        }

        // Clamp velocity
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);

        // Weapon trigger
        if (Input.GetButton("fire"))
            HUDmanager.fire(gunpoint.position, gunpoint.rotation);
        
        // Open/Close wings
        if(Input.GetButtonDown("gliderWings"))
        {
            if (wingsAreOpen) // then close wings
            {
                closingWings = true;
                wingsCloseSnd.Play();
                wings.SetActive(false);
                currentJetpackPush = jetpackPush;
                rb.drag = baseDrag;
                rb.useGravity = true;
            }
            else // open wings
            {
                openingWings = true;
                wingsOpenSnd.Play();
                wings.SetActive(true);
                currentJetpackPush = jetpackPushWings;
                rb.drag = baseWingsDrag;
                rb.useGravity = false;
                yaw = 0;
            }

            wingsAreOpen = !wingsAreOpen;
        }

        // Weapon switch/quick fire
        checkWeaponInputs(gunpoint.position, gunpoint.rotation);
    }

    void checkWeaponInputs(Vector3 pos, Quaternion rot)
    {
        if (Input.GetButtonDown("weapon1"))
            HUDmanager.switchWeapon(1);

        if (Input.GetButtonDown("weapon2"))
            HUDmanager.switchWeapon(2);

        if (Input.GetButtonDown("weapon3"))
            HUDmanager.switchWeapon(3);

        if (Input.GetButtonDown("weapon4"))
            HUDmanager.switchWeapon(4);

        if (Input.GetButtonDown("weapon5"))
            HUDmanager.switchWeapon(5);

        if (Input.GetButtonDown("weapon6"))
            HUDmanager.switchWeapon(6);

        if (Input.GetButton("fireWeapon1"))
            HUDmanager.fire(1, pos, rot);

        if (Input.GetButton("fireWeapon2"))
            HUDmanager.fire(2, pos, rot);

        if (Input.GetButton("fireWeapon3"))
            HUDmanager.fire(3, pos, rot);

        if (Input.GetButton("fireWeapon4"))
            HUDmanager.fire(4, pos, rot);

        if (Input.GetButton("fireWeapon5"))
            HUDmanager.fire(5, pos, rot);

        if (Input.GetButton("fireWeapon6"))
            HUDmanager.fire(6, pos, rot);
    }
}



