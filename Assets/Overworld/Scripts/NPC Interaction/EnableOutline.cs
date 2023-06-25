using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice; //outliner namespace

public class EnableOutline : MonoBehaviour
{
    Outline myOutline;
    void Start()
    {
        myOutline = GetComponent<Outline>();
        myOutline.enabled = false;
    }

    void OnMouseEnter()
    {
        Debug.Log("MouseOver");
        myOutline.enabled = true;
    }

    private void OnMouseExit()
    {
        myOutline.enabled = false;
    }
}