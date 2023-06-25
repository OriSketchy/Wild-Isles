using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDepresswan : DialogueClass
{
    public string[] Intro;
    public string[] StartArea;
    public string[] Forest;
    public string[] Lake;
    public string[] Beach;

    private void Start()
    {
        options[0] = Intro;
        options[1] = StartArea;
        options[2] = Forest;
        options[3] = Lake;
        options[4] = Beach;
    }
}
