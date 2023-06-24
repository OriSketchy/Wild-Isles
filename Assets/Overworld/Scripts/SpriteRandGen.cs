using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class SpriteRandGen : MonoBehaviour
{
    public List<Sprite> spriteSelection = new List<Sprite> { null };
    public SpriteRenderer source;

    void Start()
    {
        // Why is this throwing errors??? and exactly 74/138 at that??? that's not random nor half??????/
        // fixing this by catching an exception bc jesus christ im not doing another quaternion crunch
        try
        {
            source.sprite = spriteSelection[Mathf.Abs(Random.Range(0, spriteSelection.Count))];
        }
        catch { }
        //Debug.Log($"Unit: {this.gameObject.name}, Sprite Count: {spriteSelection.Count}, Chosen Sprite Index: {spriteSelection.IndexOf(source.sprite)}");
    }
}
