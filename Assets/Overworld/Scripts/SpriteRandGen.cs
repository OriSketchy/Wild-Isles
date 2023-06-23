using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandGen : MonoBehaviour
{
    public List<Sprite> spriteSelection = new List<Sprite> { null };
    public SpriteRenderer source;

    void FixedUpdate()
    {
        source.sprite = spriteSelection[Random.Range(0, spriteSelection.Count)];
    }
}
