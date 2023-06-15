using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice; //outliner namespace

public class EnableOutline : MonoBehaviour
{
    void Start()
    {
        GetComponent<Outline>().enabled = false;
    }

    void OnMouseEnter()
    {
        Debug.Log("MouseOver");
        GetComponent<Outline>().enabled = true;
    }

    private void OnMouseExit()
    {
        GetComponent<Outline>().enabled = false;
    }
}