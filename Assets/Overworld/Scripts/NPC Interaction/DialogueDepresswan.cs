using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDepresswan : DialogueClass
{
    public List<string> Intro = new List<string>();
    public List<string> StartArea = new List<string>();
    public List<string> Forest = new List<string>();
    public List<string> Lake = new List<string>();
    public List<string> Beach = new List<string>();

    private void Start()
    {
        options[0] = Intro;
        options[1] = StartArea;
        options[2] = Forest;
        options[3] = Lake;
        options[4] = Beach;
    }
}
