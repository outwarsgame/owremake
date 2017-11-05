using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WPManager : MonoBehaviour
{
    public weapon[] wps;
    public Text selectedWPText;
    public AudioSource switchClick;
    
    weapon currentWP;
    
    void Start()
    {
        currentWP = wps[0].select(selectedWPText);
    }

    public void switchWeapon(int nb)
    {
        nb--;

        currentWP.unselect();
        currentWP = wps[nb].select(selectedWPText);

        switchClick.Play();
    }

    public void fire(Vector3 pos, Quaternion rot)
    {
        currentWP.fire(pos, rot);
    }

    public void fire(int slotNb, Vector3 pos, Quaternion rot)
    {
        slotNb--;

        wps[slotNb].fire(pos, rot);
    }

    public bool refill(Powerup.PowerupType poType)
    {
        bool refillRequired = false;

        switch (poType)
        {
            case Powerup.PowerupType.grenade:
                foreach (weapon w in wps)
                    if (w.getWeaponType() == weaponData.WPType.grenade)
                        if (w.refill())
                            refillRequired = true;
                break;

            case Powerup.PowerupType.mine:
                foreach (weapon w in wps)
                    if (w.getWeaponType() == weaponData.WPType.mine || w.getWeaponType() == weaponData.WPType.remoteMine)
                        if (w.refill())
                            refillRequired = true;
                break;

            case Powerup.PowerupType.missile:
                foreach (weapon w in wps)
                    if (w.getWeaponType() == weaponData.WPType.missile)
                        if (w.refill())
                            refillRequired = true;
                break;

            case Powerup.PowerupType.secondary:
                foreach (weapon w in wps)
                    if (w.getWeaponType() == weaponData.WPType.secondary)
                        if (w.refill())
                            refillRequired = true;
                break;
        }

        return refillRequired;
    }
}

