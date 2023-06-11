using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)] private float speed;

    void FixedUpdate()
    {
        //vector is automatically set to 0, 0, 0 so player doesnt move without input
        Vector3 direction = Vector3.zero;

        //Add up player input
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
        
        //Execute player input
        transform.position += direction.normalized * speed * Time.fixedDeltaTime; 
    }
}
