using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform Stare(GameObject enemy)
    {
        transform.LookAt(enemy.transform);
        return this.transform;
    }
}
