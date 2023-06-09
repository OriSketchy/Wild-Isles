using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 5)] private float speed;
    public GameObject character;

    void Update() 
    {    
        
        if (Input.GetKey("d"))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        if (Input.GetKey("a"))
        {
            transform.position += Vector3.left* speed * Time.deltaTime;
        }
        if (Input.GetKey("w"))
        {
            transform.position += Vector3.forward * speed * Time.deltaTime;
        }
        if (Input.GetKey("s"))
        {
            transform.position += Vector3.back* speed * Time.deltaTime;
        }
    }
}
