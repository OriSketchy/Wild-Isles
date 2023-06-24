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
        if (playerUnit.items[slot] == null)
        {
            this.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.GetComponent<Button>().interactable = true;
        }
    }
}
