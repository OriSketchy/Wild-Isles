using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCheck : MonoBehaviour
{
    public int slot;
    public Unit playerUnit;
    private void Awake()
    {
        RefreshButton();
    }
    public void RefreshButton()
    {
        if (playerUnit.items[slot-4] == null)
        {
            this.GetComponent<Button>().interactable = false;
        }
        else if (playerUnit.items[slot-4] != null)
        {
            this.GetComponent<Button>().interactable = true;
        }
    }
}
