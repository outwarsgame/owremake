using UnityEngine;
using System.Collections;

public class UserControl : MonoBehaviour
{
    public float strafeSpeed, walkSpeed, yawSpeed;
    public float maxWingsOpenDelta, baseWingsDrag, maxStallDelta, maxGravityEffect;
    public float jetpackPush, jetpackPushWings, maxVelocity, stallWarningVelocity, stallCriticalVelocity;
    public float flySensitivity, wingsMomentumTransferFactor;

    public float maxCameraSpeed;

	public Transform aim, gunpoint, player;
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

	void Update ()
    {
        if (openingWings)
        {
            player.localRotation = Quaternion.Euler(90, 0, 0); // Smooth this
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
			if(HUDmanager.isJumping())
			{
				stalling = false;
				rb.useGravity = false;
			}

			if (rb.velocity.magnitude < stallWarningVelocity)
            {
                if (!stallWarning.isPlaying)
                    stallWarning.Play();

				if (rb.velocity.magnitude < stallCriticalVelocity)
                {
                    stalling = true;
					rb.useGravity = true;
				}
            }

			if(stalling)
				player.localRotation = Quaternion.RotateTowards(player.localRotation, Quaternion.Euler(180, player.localRotation.y, player.localRotation.z), maxStallDelta);

			// Yaw
			float yawChange = Input.GetAxis("x") * flySensitivity;
			Vector3 yaw_axis = -1.0f * player.up;  float c_yaw = Mathf.Cos(yawChange / 2.0f); float s_yaw = Mathf.Sin(yawChange / 2.0f);
			Quaternion yaw = new Quaternion(s_yaw * yaw_axis.x, s_yaw * yaw_axis.y, s_yaw * yaw_axis.z, c_yaw);

			// Pitch
			float pitchChange = Input.GetAxis("z") * flySensitivity;
			Vector3 pitch_axis = player.right; float c_pitch = Mathf.Cos(pitchChange / 2.0f); float s_pitch = Mathf.Sin(pitchChange / 2.0f);
			Quaternion pitch = new Quaternion(s_pitch * pitch_axis.x, s_pitch * pitch_axis.y, s_pitch * pitch_axis.z, c_pitch);

			// Apply rotations
			player.rotation = yaw * pitch * player.rotation;

			transform.Rotate(0, Input.GetAxis("Mouse X"), 0); // move mouse horizontally

			// aim vertically
			aim.RotateAround(transform.position, -1.0f * transform.right, Input.GetAxis("Mouse Y"));
			aim.localRotation.ToAngleAxis(out float theta_mouse_y, out Vector3 u_mouse_y);
			aim.localRotation = Quaternion.AngleAxis(Mathf.Clamp(theta_mouse_y, -85.0f, 85.0f), u_mouse_y);

			// aim horizontally
			//aim.RotateAround(transform.position, transform.up, Input.GetAxis("Mouse X"));
			//aim.localRotation.ToAngleAxis(out float theta_mouse_x, out Vector3 u_mouse_x);
			//aim.localRotation = Quaternion.AngleAxis(theta_mouse_x, u_mouse_x);
		}
        else // Walk
        {
            float x_mvt = Input.GetAxis("x") * strafeSpeed * armorSpeedFactor;
            float z_mvt = Input.GetAxis("z") * walkSpeed * armorSpeedFactor;

            transform.Translate(x_mvt, 0, z_mvt);
            transform.Rotate(0, Input.GetAxis("Mouse X"), 0); // move mouse horizontally

			aim.RotateAround(transform.position, -1.0f * transform.right, Input.GetAxis("Mouse Y")); // move mouse vertically
			aim.localRotation.ToAngleAxis(out float theta, out Vector3 u);
			aim.localRotation = Quaternion.AngleAxis( Mathf.Clamp(theta, -85.0f, 85.0f), u );
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
				//aim.localRotation = aim.localRotation.
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



