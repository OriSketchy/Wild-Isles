using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class StartGame : MonoBehaviour
{
    public TMP_InputField nameBox;
    public Button myButton;
    public GameObject errorText;
    void FixedUpdate()
    {
        // check for fail conditions: empty string, non-letter/number characters, over 8 letters
        if(nameBox.text != string.Empty && 
            Regex.IsMatch(nameBox.text, @"^[a-zA-Z0-9]+$") && // "regex" is checking a pattern rather than comparing each element
            nameBox.text.Length <= 8) // regex is frien
        {
            myButton.interactable = true;
            errorText.SetActive(false);
        }
        else
        {
            myButton.interactable = false;
            errorText.SetActive(true);
        }
    }
}