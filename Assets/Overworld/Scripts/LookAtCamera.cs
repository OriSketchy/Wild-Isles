using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    Transform cam;
    private void Start() 
    {
        cam = Camera.main.transform;
    }
    void LateUpdate() 
    {
        transform.LookAt(transform.position + cam.forward);
    }
}
