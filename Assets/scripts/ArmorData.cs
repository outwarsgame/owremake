using UnityEngine;
using System.Collections;

public class ArmorData : MonoBehaviour
{
    public enum ArmorType { scout, combat, assault, dreadnaut };

    public ArmorType armorType;

    /// <summary>
    /// TODO not measured yet
    /// </summary>
    public float hitPoints;

    /// <summary>
    /// TODO not measured yet
    /// </summary>
    public float armorPoints;

    /// <summary>
    /// Time in seconds for a full jump recharge
    /// </summary>
    public float jumpRechargeTime;
    
    /// <summary>
    /// 100 points are consumed for 1 second of jump. This is just a convention.
    /// </summary>
    public float maxJumpPoints;
    
    /// <summary>
    /// Assault is the slowest and the reference set at 1.
    /// </summary>
    public float speedFactor;

    /// <summary>
    /// What does this beauty looks like?
    /// </summary>
    public Sprite armorImage;
}
