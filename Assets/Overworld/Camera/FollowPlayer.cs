using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 4f;
    private Vector3 velocity;
    [SerializeField]
    bool builtin = true;

    private void LateUpdate()
    {
        if(builtin)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);
        }
        else
        {
            Vector3 distance = target.position + offset - transform.position;
            Vector3 move = distance * smoothTime * Time.deltaTime;
            transform.position += move;
        }
    }
}