using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// List of menu states
public enum MenuState { MAIN, PLAY, OPTIONS, CREDITS }

public class MainMenu : MonoBehaviour
{
    public GameObject keyAssetParent;
    public GameObject playParent;
    public GameObject optionsParent;
    public GameObject creditsParent;
    public StartGame inputData;

    private MenuState state;
    public void OnExitButton()
    {
        // Exit game
        Application.Quit();
        Debug.Log("Exitted game");
    }
    public void OnPlayButton()
    {
        // Go to input then play screen (WRITE TO FILE)
        keyAssetParent.SetActive(false);
        playParent.SetActive(true);
        state = MenuState.PLAY;
    }
    public void StartGame()
    {
        // start game. use PlayerPrefs to save data
        PlayerPrefs.SetString("name", inputData.chosenName);
        PlayerPrefs.SetInt("score", 0);

        SceneManager.LoadScene("Overworld");
    }
    public void OnOptionsButton()
    {
        // Go to options screen
        keyAssetParent.SetActive(false);
        optionsParent.SetActive(true);
        state = MenuState.OPTIONS;
    }
    public void OnCreditsButton()
    {
        // Go to credits screen
        keyAssetParent.SetActive(false);
        creditsParent.SetActive(true);
        state = MenuState.CREDITS;
    }
    public void OnBackButton()
    {
        // Go to main screen
        switch (state)
        {
            case MenuState.CREDITS:
                creditsParent.SetActive(false);
                break;
            case MenuState.OPTIONS:
                optionsParent.SetActive(false);
                break;
            case MenuState.PLAY:
                playParent.SetActive(false);
                break;
        }
        keyAssetParent.SetActive(true);
        state = MenuState.MAIN;
    }
}