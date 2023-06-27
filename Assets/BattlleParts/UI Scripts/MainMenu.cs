using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject keyAssetParent;
    public GameObject playParent;
    public GameObject optionsParent;
    public GameObject creditsParent;
    public void OnExitButton()
    {
        // Exit game
        Application.Quit();
    }
    public void OnPlayButton()
    {
        // Go to input then play screen (WRITE TO FILE)
        keyAssetParent.SetActive(false);
        playParent.SetActive(true);
    }
    public void OnOptionsButton()
    {
        // Go to options screen
        keyAssetParent.SetActive(false);
        optionsParent.SetActive(true);
    }
    public void OnCreditsButton()
    {
        // Go to credits screen
        keyAssetParent.SetActive(false);
        creditsParent.SetActive(true);
    }
    public void OnBackButton()
    {
        // Go to main screen

    }
}
