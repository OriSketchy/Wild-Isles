using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDavide : DialogueClass
{
    public string[] Intro;
    public string[] Guide;
    public string[] Loop;
    private void Start()
    {
        options[0] = Intro;
        options[1] = Guide;
        options[2] = Loop;
    }
}
