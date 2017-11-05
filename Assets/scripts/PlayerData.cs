using UnityEngine;
using System.Collections;

public class PlayerData : MonoBehaviour
{
    public ArmorData armorData;

    ArmorData.ArmorType armorType;
    float hitPoints, armorPoints, maxJumpPoints;
    float jumpRechargeTime;
    float speedFactor;
    Sprite armorImage;

    void Awake()
    {
        armorType = armorData.armorType;
        hitPoints = armorData.hitPoints;
        armorPoints = armorData.armorPoints;
        maxJumpPoints = armorData.maxJumpPoints;
        jumpRechargeTime = armorData.jumpRechargeTime;
        speedFactor = armorData.speedFactor;
        armorImage = armorData.armorImage;
    }

    public float getSpeedFactor()
    {
        return speedFactor;
    }

    public float getHP()
    {
        return hitPoints;
    }

    public Sprite getArmorImage()
    {
        return armorImage;
    }

    public float getJumpRechargeTime()
    {
        return jumpRechargeTime;
    }

    public float getMaxJumpPoints()
    {
        return maxJumpPoints;
    }
}