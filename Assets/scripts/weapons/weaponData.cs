using UnityEngine;

public class weaponData : MonoBehaviour
{
    public enum WPType { main, secondary, grenade, missile, mine, remoteMine };

    /// <summary>
    /// The name of this weapon to be shown in the HUD
    /// </summary>
    public string nameWP;

    /// <summary>
    /// To which category this weapon belongs?
    /// </summary>
    public WPType type_weapon;

    /// <summary>
    /// Maximum possible ammo for this weapon
    /// </summary>
    public int maxAmmo;

    /// <summary>
    /// Current available ammo
    /// </summary>
    public int currentAmmo;

    /// <summary>
    /// The icon of this weapon to be shown in the HUD
    /// </summary>
    public Sprite sprite;

    /// <summary>
    /// A prefab to be instantiated as a round/projectile for this weapon. Expected to have a "shot" script component.
    /// </summary>
    public GameObject projectile;

    /// <summary>
    /// Time in seconds for one ammo regeneration (origin. pulse rifle only). 0 for no regeneration.
    /// </summary>
    public float rechargeTimeInSeconds;

    /// <summary>
    /// Time in seconds between two shots for this weapon
    /// </summary>
    public float cooldownInSeconds;

    /// <summary>
    /// The noise this weapon makes when it fires one shot
    /// </summary>
    public AudioClip weaponFireAudioClip;
}
