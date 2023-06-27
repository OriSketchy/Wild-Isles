using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class defaultButtonCheck : MonoBehaviour
{
    public void ButtonDisable()
    {
        this.GetComponent<Button>().interactable = false;
    }
    public void ButtonEnable()
    {
        this.GetComponent<Button>().interactable = true;
    }
}
