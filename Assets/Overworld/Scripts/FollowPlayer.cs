using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothTime = 4f;
    private Vector3 velocity;

    public bool battleEngaged = false;

    private void FixedUpdate()
    {
        if (battleEngaged)
        {
            return;
        }
        else
        {
            Vector3 distance = target.position + offset - transform.position;
            Vector3 move = distance * smoothTime * Time.deltaTime;
            transform.position += move;
        }
    }
}
