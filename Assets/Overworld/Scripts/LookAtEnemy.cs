using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtEnemy : MonoBehaviour
{
    public Transform Stare(GameObject enemy)
    {
        transform.LookAt(enemy.transform);
        return this.transform;
    }
}
