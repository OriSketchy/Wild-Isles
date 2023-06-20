using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Text levelText;
    public Slider hpSlider;

    // fetching data for HUD text
    // needs tuning
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        // BALANCE/ADJUST THIS
        levelText.text = "Lvl " + (Mathf.Round((float)unit.unitXP / 100) * 100);
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }
    // update HP
    public void SetHP(int hp) 
    { 
        hpSlider.value = hp;
    }
}
