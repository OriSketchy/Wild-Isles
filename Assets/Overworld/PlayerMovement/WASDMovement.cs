using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)] private float speed;

    void FixedUpdate()
    {
        Vector3 direction = Vector3.zero;

        if (Input.GetKey("d"))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey("a"))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey("w"))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey("s"))
        {
            direction += Vector3.back;
        }
        
        transform.position += direction.normalized * speed * Time.fixedDeltaTime; 
    }
}
