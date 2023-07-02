using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponCheck : MonoBehaviour
{
    public int slot;
    public Unit playerUnit;
    public void RefreshButton()
    {
        // using a try/catch to prevent trying to assign a var as null
        // if the inventory slot contains an item that lines up with this button, it will be click-able
        try
        {
            var invSlot = playerUnit.items[slot];
            if (invSlot.damageType-1 == slot)
            {
                this.GetComponent<Button>().interactable = true;
            }
            else
            {
                this.GetComponent<Button>().interactable = false;
            }
        }
        catch
        {
            this.GetComponent<Button>().interactable = false;
        }
    }
}
