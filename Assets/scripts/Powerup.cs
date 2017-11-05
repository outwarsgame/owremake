using UnityEngine;
using System.Collections;

public class Powerup : MonoBehaviour
{
    public enum PowerupType { armor, grenade, medkit, mine, missile, secondary, tracker };

    public PowerupType powerupType;
    public float spawnDelay;

    public AudioSource[] audioSources;
    public AudioClip genericPickup, armorPickup, trackerPickup;
    public AudioClip[] medkitPickup;
    public GameObject armorPrefab, grenadePrefab, medkitPrefab, minePrefab, missilePrefab, secondaryPrefab, trackerPrefab;

    GameObject physicalPowerup;
    bool powerupHasBeenPicked, respawning;
    HUDManager hudman;

    // Initially, there should be a gameobject representing the physical powerup
    void Awake()
    {
        powerupHasBeenPicked = false;
        respawning = false;

        physicalPowerup = transform.GetChild(0).gameObject;

        hudman = GameObject.Find("HUD").GetComponent<HUDManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (powerupHasBeenPicked || other.gameObject.tag != "Player")
            return;

        switch (powerupType)
        {
            case PowerupType.armor:
                if (!hudman.pickPowerup(PowerupType.armor))
                    break;

                powerupHasBeenPicked = true;

                audioSources[0].clip = armorPickup;
                audioSources[0].Play();

                StartCoroutine(respawn(armorPrefab));
                break;

            case PowerupType.grenade:
                if (!hudman.pickPowerup(PowerupType.grenade))
                    break;

                powerupHasBeenPicked = true;

                audioSources[0].clip = genericPickup;
                audioSources[0].Play();

                StartCoroutine(respawn(grenadePrefab));
                break;

            case PowerupType.medkit:
                if (!hudman.pickPowerup(PowerupType.medkit))
                    break;

                powerupHasBeenPicked = true;

                audioSources[0].clip = medkitPickup[0];
                audioSources[1].clip = medkitPickup[1];
                audioSources[0].Play();
                audioSources[1].Play();

                StartCoroutine(respawn(medkitPrefab));
                break;

            case PowerupType.mine:
                if (!hudman.pickPowerup(PowerupType.mine))
                    break;

                powerupHasBeenPicked = true;

                audioSources[0].clip = genericPickup;
                audioSources[0].Play();

                StartCoroutine(respawn(minePrefab));
                break;

            case PowerupType.missile:
                if (!hudman.pickPowerup(PowerupType.missile))
                    break;

                powerupHasBeenPicked = true;

                audioSources[0].clip = genericPickup;
                audioSources[0].Play();

                StartCoroutine(respawn(missilePrefab));
                break;

            case PowerupType.secondary:
                if (!hudman.pickPowerup(PowerupType.secondary))
                    break;

                powerupHasBeenPicked = true;

                audioSources[0].clip = genericPickup;
                audioSources[0].Play();

                StartCoroutine(respawn(secondaryPrefab));
                break;

            case PowerupType.tracker:
                if (!hudman.pickPowerup(PowerupType.tracker))
                    break;

                powerupHasBeenPicked = true;

                audioSources[0].clip = trackerPickup;
                audioSources[0].Play();

                StartCoroutine(respawn(trackerPrefab));
                break;
        }

        if(powerupHasBeenPicked)
            Destroy(physicalPowerup);
    }

    IEnumerator respawn(GameObject prefab)
    {
        if (respawning)
            yield break;

        respawning = true;

        yield return new WaitForSeconds(spawnDelay);

        physicalPowerup = (GameObject)Instantiate(prefab, transform.position, new Quaternion(0, 0, 0, 0), gameObject.transform);

        powerupHasBeenPicked = false;
        respawning = false;
    }
}
