using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI levelText;
    public Slider hpSlider;

    // fetching data for HUD text
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        // BALANCE/ADJUST THIS
        levelText.text = "LVL: " + (Mathf.Round((float)unit.unitXP / 100) * 100);
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }
    // update HP
    public void SetHP(int hp) 
    { 
        hpSlider.value = hp;
    }
}
