using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitXP;

    public int maxHP;
    public int currentHP;

    public int maxSP;
    public int currentSP;

    // 1 = stab, 2 = grab, 3 = thwab, 0 = strong against all/Duffin
    // 4 = SP stab, 5 = SP grab 6 = SP thwab
    public int damageType;
    public int damageWeakness;
    public float damageBase;

    // 0 = player, 1 = bag bunny, 2 = box frog, 3 = microplastic slime
    // is there a better way to do this? yeah
    // am i going to change this? not unless it becomes a liability
    public int unitID;

    // Defining what items Duffin has (allows SP buttons to be pressed)
    // stab, grab, thwab
    public List<bool> items = new List<bool> { false, false, false };
    public List<ItemConsume> itemConsumes = new List<ItemConsume> { null, null, null };

    public bool TakeDamage(float dmg, int dmgType, int damMod = 0)
    {
        dmg += damMod;

        // cleans scrap attack (all bonuses are done externally)
        if (dmgType >= 4) { dmgType -= 3; }
        // checks weakness with cleaned damage type
        if (dmgType == damageWeakness) dmg = Mathf.Ceil(dmg * 1.5f);

        // crit chance
        if (Random.Range(0, 10) == 1) dmg = Mathf.Ceil(dmg * 1.5f);

        // deals the damage
        currentHP -= (int)dmg;
        // prevents dropping to 0 (death works in external script)
        if (currentHP <= 0)
            return true;
        else
            return false;
    }
    public void HealUnit(int itemSlot)
    {
        ItemConsume currentItem = itemConsumes[itemSlot];

        currentHP += currentItem.healAmount;
        if (currentHP >= 0) currentHP = maxHP;
    }
}