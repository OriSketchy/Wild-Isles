using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOutline : MonoBehaviour
{
    Outline outline;

    void Start()
    {
        GetComponent<Outline>().enabled = false;
    }

    void OnMouseOver()
    {
        GetComponent<Outline>().enabled = true;
    }
}
