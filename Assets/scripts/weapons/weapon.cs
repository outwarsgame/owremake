using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class weapon : MonoBehaviour
{
    public weaponData wpdata; // set a prefab in the editor for now
    public GameObject highlight; // The yellow bracket surrounding the slot UI
    public AudioSource ammoDepleted;
    public GameObject remoteExplosionPrefab;
    public AudioSource weaponFire;

    // UI elements
    Text ammotxt;
    Image WPimage;

    string nameWP;
    int maxAmmo, currentAmmo;
    Sprite sprite;
    GameObject projectile;
    shot projectileManager;
    float rechargeTimeInSeconds, cooldownInSeconds;
    weaponData.WPType type_weapon;
    AudioClip weaponFireAudioClip;
    
    bool regeneratingAmmo = false;
    bool cooldowned = true;
    bool clockStarted = false;

    GameObject currentRemoteMine = null;
    AudioSource explosionAudioSource = null;

    void Awake()
    {
        WPimage = GetComponent<Image>();
        ammotxt = GetComponentInChildren<Text>();
        
        nameWP = wpdata.nameWP;
        maxAmmo = wpdata.maxAmmo;
        currentAmmo = wpdata.currentAmmo;
        sprite = wpdata.sprite;
        projectile = wpdata.projectile;
        projectileManager = projectile.GetComponent<shot>(); // the provided GameObject projectile prefab is expected to have a "shot" script attached to it
        rechargeTimeInSeconds = wpdata.rechargeTimeInSeconds;
        cooldownInSeconds = wpdata.cooldownInSeconds;
        type_weapon = wpdata.type_weapon;
        weaponFireAudioClip = wpdata.weaponFireAudioClip;

        WPimage.sprite = sprite;
        ammotxt.text = "" + currentAmmo;

        weaponFire.clip = weaponFireAudioClip;
    }

    void Update()
    {
        if(rechargeTimeInSeconds > 0 && !regeneratingAmmo && currentAmmo < maxAmmo)
        {
            regeneratingAmmo = true;
            StartCoroutine(regenerateAmmo());
        }
    }

    IEnumerator regenerateAmmo()
    {
        yield return new WaitForSeconds(rechargeTimeInSeconds);
        currentAmmo++;
        ammotxt.text = "" + currentAmmo;
        regeneratingAmmo = false;
    }

    public weapon select(Text selectedWPText)
    {
        highlight.SetActive(true);
        selectedWPText.text = nameWP;
        return this;
    }

    public void unselect()
    {
        highlight.SetActive(false);
    }

    public void fire(Vector3 pos, Quaternion rot)
    {
        if (type_weapon == weaponData.WPType.remoteMine)
        {
            fire_remote(pos, rot);
            return;
        }

        if (cooldowned)
        {
            StartCoroutine(cooldownClock());
            cooldowned = false;

            if (currentAmmo > 0)
            {
                weaponFire.Play();
                Instantiate(projectile, pos, rot);

                currentAmmo--;
                ammotxt.text = "" + currentAmmo;
            }
            else
            {
                ammoDepleted.Play();
            }
        }
    }
    
    void fire_remote(Vector3 pos, Quaternion rot)
    {
        if (cooldowned)
        {
            StartCoroutine(cooldownClock());
            cooldowned = false;

            if (currentRemoteMine)
            {
                Instantiate(remoteExplosionPrefab, currentRemoteMine.transform.position, Quaternion.Euler(0, 0, 0));
                explosionAudioSource.Play();
                Destroy(currentRemoteMine);
            }
            else if(currentAmmo > 0)
            {
                weaponFire.Play();
                currentRemoteMine = (GameObject)Instantiate(projectile, pos, rot);
                explosionAudioSource = currentRemoteMine.GetComponentInChildren<AudioSource>();
                currentAmmo--;
                ammotxt.text = "" + currentAmmo;
            }   
        }  
    }

    IEnumerator cooldownClock()
    {
        if (clockStarted)
            yield break;

        clockStarted = true;
        yield return new WaitForSeconds(cooldownInSeconds);
        cooldowned = true;
        clockStarted = false;
    }

    public weaponData.WPType getWeaponType()
    {
        return type_weapon;
    }

    public bool refill()
    {
        if (currentAmmo == maxAmmo)
            return false;

        currentAmmo = maxAmmo;
        ammotxt.text = "" + currentAmmo;
        return true;
    }
}
