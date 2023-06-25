using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnRegion : MonoBehaviour
{
    [SerializeField] List<Unit> enemySpawns = new List<Unit>();
    [SerializeField] int radius;
    SphereCollider myCollider;

    public void Spawn()
    {
        // sets own collider (prefab references are kinda annoying) and adjusts its radius to be a spawn area (rather than trigger area)
        myCollider = this.GetComponent<SphereCollider>();
        myCollider.radius = radius;
        // spawns each enemy at a random coordinate within spawn area (disregarding Y)
        foreach(Unit enemy in enemySpawns)
        {
            Instantiate(enemy, RandomPointInBounds(myCollider.bounds), Quaternion.identity);
        }
        // disables spawn entirely (use for tracking spawn success)
        this.gameObject.SetActive(false);
    }
    // function that returns a random coordinate each time it's called (disregard Y axis)
    public static Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            1,
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }
}
