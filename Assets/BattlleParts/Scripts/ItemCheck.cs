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
        if (playerUnit.itemConsumes.Count-1 < slot)
        {
            this.GetComponent<Button>().interactable = false;
            childImage.gameObject.SetActive(false);
        }
        else
        {
            // add item image to child to this button
            childImage.gameObject.SetActive(true);
            childImage.sprite = playerUnit.itemConsumes[slot].texture;
            // maybe healing stats too?
            this.GetComponent<Button>().interactable = true;
        }
    }
}
