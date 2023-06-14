using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public string unitName;
    public int unitLevel;

    public int maxHP;
    public int currentHP;

    // 1 = stab, 2 = grab, 3 = thwab, 0 = strong against all/Duffin
    // 4 = SP stab, 5 = SP grab 6 = SP thwab
    public int damageType;
    public int damageWeakness;
    public float damageBase;

    // 0 = player, 1 = bag bunny, 2 = box frog, 3 = microplastic slime
    // is there a better way to do this? yeah
    // am i going to change this? not unless it becomes a liability
    public int unitID;

    public bool TakeDamage(float dmg, int dmgType, int damMod = 0)
    {
        dmg += damMod;

        if (dmgType > 4) { if (dmgType == damageWeakness) dmg = Mathf.Ceil(dmg * 1.5f); }
        if (dmgType <= 4) { if (dmgType == damageWeakness) dmg = Mathf.Ceil(dmg * 1.5f); }

        if (Random.Range(0, 10) == 1) dmg = Mathf.Ceil(dmg * 1.5f);

        currentHP -= (int)dmg;
        if (currentHP <= 0)
            return true;
        else
            return false;
    }
    public void HealUnt(int amount)
    {
        currentHP += amount;
        if (currentHP >= 0) currentHP = maxHP;
    }
}