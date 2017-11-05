using UnityEngine;
using System.Collections;
using System;

public class HUDManager : MonoBehaviour
{
    WPManager WPM;
    BioStatsManager bioStatsManager;
    JumpManager jumpManager;

    void Awake()
    {
        WPM = GetComponentInChildren<WPManager>(); // There should be one and only one WPManager
        bioStatsManager = GetComponentInChildren<BioStatsManager>(); // There should be one and only one BioStatsManager
        jumpManager = GetComponentInChildren<JumpManager>();
    }

    public void switchWeapon(int nb)
    {
        WPM.switchWeapon(nb);
    }

    public void fire(Vector3 pos, Quaternion rot)
    {
        WPM.fire(pos, rot);
    }

    public void fire(int slotNb, Vector3 pos, Quaternion rot)
    {
        WPM.fire(slotNb, pos, rot);
    }

    public void damage(float dam)
    {
        bioStatsManager.damage(dam);
    }

    public void setHitPoints(float HP)
    {
        bioStatsManager.setHP(HP);
    }

    public void setArmorImage(Sprite armorImage)
    {
        bioStatsManager.setArmorSprite(armorImage);
    }

    public void setArmorPoints(float armorPoints)
    {
        // TODO
    }

    public void setWingsState(bool wingsAreOpen)
    {
        jumpManager.setWingsState(wingsAreOpen);
    }

    public void setJumpStats(float maxJumpPoints, float jumpRechargeTime)
    {
        jumpManager.setJumpStats(maxJumpPoints, jumpRechargeTime);
    }

    public bool isJumping()
    {
        return jumpManager.isJumping();
    }

    public bool pickPowerup(Powerup.PowerupType potype)
    {
        switch(potype)
        {
            case Powerup.PowerupType.armor:
                // todo replenish armor
                return true;

            case Powerup.PowerupType.grenade:
                return WPM.refill(Powerup.PowerupType.grenade);

            case Powerup.PowerupType.medkit:
                // todo replenish HP
                return true;

            case Powerup.PowerupType.mine:
                return WPM.refill(Powerup.PowerupType.mine);
                
            case Powerup.PowerupType.missile:
                return WPM.refill(Powerup.PowerupType.missile);

            case Powerup.PowerupType.secondary:
                return WPM.refill(Powerup.PowerupType.secondary);

            case Powerup.PowerupType.tracker:
                // todo modify radar
                return true;
        }

        return false;
    }
}
