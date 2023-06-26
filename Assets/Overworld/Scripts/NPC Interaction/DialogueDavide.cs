using Palmmedia.ReportGenerator.Core.Parser.Analysis;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DialogueDavide : DialogueClass
{
    public List<string> Intro = new List<string>();
    public List<string> Guide = new List<string>();
    public List<string> Loop = new List<string>();

    public DetectClick myClick;
    //public string[][] options;

    // Change based on where Depresswan is (if handling Depresswan)
    //public int customIndex;
    //public int maxIndex;
    private void Start()
    { 
        options = new List<List<string>>() { Intro, Guide, Loop };
        //options[0] = Intro;
        //options[1] = Guide;
        //options[2] = Loop;

        myClick.dialogueGrid = options;
    }
}
