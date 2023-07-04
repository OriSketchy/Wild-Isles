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
    public Slider spSlider;

    // fetching data for HUD text
    public void SetHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        // BALANCE/ADJUST THIS
        levelText.text = "LVL: " + Mathf.Round((float)unit.unitXP / 100);

        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;

        spSlider.maxValue = unit.maxSP;
        spSlider.value = unit.currentSP;
    }
    // update HP
    public void UpdateHUD(Unit unit) 
    { 
        hpSlider.value = unit.currentHP;
        spSlider.value = unit.currentSP;
    }
}
