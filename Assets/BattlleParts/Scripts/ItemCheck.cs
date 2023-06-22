using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCheck : MonoBehaviour
{
    public int slot;
    public Image childImage;
    public Unit playerUnit;
    private void Awake()
    {
        if (playerUnit.itemConsumes[slot] == null)
        {
            this.GetComponent<Button>().interactable = false;
            childImage.gameObject.SetActive(false);
        }
        else
        {
            // add item image to child to this button
            childImage.gameObject.SetActive(true);
            childImage = playerUnit.itemConsumes[slot].GetComponent<Image>();
            // maybe healing stats too?
            this.GetComponent<Button>().interactable = true;
        }
    }
}
