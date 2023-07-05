using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks.Sources;
using UnityEngine;

public class Dukebox : MonoBehaviour
{
    public AudioClip overworld;
    public AudioClip dialogue;
    public AudioClip menu;

    public bool isOverworld;

    private bool overworldToggle = false;

    private AudioSource mySource;
    private void Start()
    {
        mySource = this.GetComponent<AudioSource>();
        if(isOverworld)
        {
            mySource.clip = overworld;
        }
        else
        {
            mySource.clip = menu;
        }
        mySource.Play();
        mySource.loop = true;
    }

    public void OverworldToggle()
    {
        if (overworldToggle)
        {
            mySource.clip = overworld;
            overworldToggle = false;
        }
        else if (!overworldToggle)
        {
            mySource.clip = dialogue;
            overworldToggle = true;
        }
        mySource.Play();
        mySource.loop = true;
    }
}
