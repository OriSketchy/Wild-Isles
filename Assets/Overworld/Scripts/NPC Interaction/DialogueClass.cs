using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueClass : MonoBehaviour
{
    public List<List<string>> options;
    public DetectClick myClick;

    // Change based on where Depressawn is (using his Prefab)
    public int customIndex;
    public int maxIndex;
}
