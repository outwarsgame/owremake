using UnityEngine;
using UnityEngine.UI;

public class BioStatsManager : MonoBehaviour
{
    public Image armorImage;

    float maxHP, currentHP;

    public void setHP(float HP)
    {
        maxHP = HP;
        currentHP = maxHP;
    }

    public void setArmorSprite(Sprite armorSprite)
    {
        armorImage.sprite = armorSprite;
    }

    public void damage(float dam)
    {
        // TODO
    }
}
