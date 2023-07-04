using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//use this as a class for end menu types

public class MenuEnd : MonoBehaviour
{
    public TextMeshProUGUI scoreBox;
    public TextMeshProUGUI nameBox;

    private int playerScore;
    private string playerName;

    // function that gets player data and puts it in the respective boxes
    private void Start()
    {
        scoreBox.text = $"{PlayerPrefs.GetInt("score")}";
        nameBox.text = $"{PlayerPrefs.GetString("name")}'s Final Score:";
    }
    public void EndGame()
    {
        // end the game
        Application.Quit();
    }
}
