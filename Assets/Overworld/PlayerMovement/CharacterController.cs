using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Range(1, 100)] public int moveSpeed;
    public float ForwardInput { get; set; }
    new private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // private void FixedUpdate()
    // {
    //     ProcessActions();
    // }

    // private void ProcessActions()
    // {
    //     //move forward and back
    //     rigidbody.velocity += transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) * moveSpeed;
    // }

     private void FixedUpdate()
    {
        //move forward and back
        rigidbody.velocity += transform.forward * Mathf.Clamp(ForwardInput, -1f, 1f) * moveSpeed;
    }

}
