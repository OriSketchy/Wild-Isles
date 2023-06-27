using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class LeaderboardHandler : MonoBehaviour
{
    string text = string.Empty;
    public TextMeshProUGUI leaderBox;

    public string textDir;
    public string directory;

    public StreamReader reader;

    private void Start()
    {
       // reader = new StreamReader(directory + textDir, true);
    }

    // To organise list:
    // - split each line into an array (words 0, score 1)
    // - put each array into a 2D array (words [x][0], score [x][1])
    // - sort 2D array by score
    // - write array back to file, combining each column into 1 line
    public void SortScores()
    {
        reader = new StreamReader(directory + textDir);
        // Use this to read and extract file
        //string input = reader.ReadToEnd();
        string[][] sortingArray = new string[0][];
        sortingArray[0][0] = "DEBUG";
        sortingArray[0][1] = "10000";

        // Compare each line to neighbours. When position is found, move all items from bottom of the array down one and fill in blank with the line

        // runs though each array element. Gets sorted position.
        for (int i = 0; i <= sortingArray.Length; i++)
        {
            // split name/score (index 0, 1)
            string[] newLine = reader.ReadLine().Split(" ");
            int chosenIndex;

            if (int.Parse(newLine[1]) <= int.Parse(sortingArray[i][1]))
            {
                chosenIndex = i;
                for (int a = sortingArray.Length; a >= chosenIndex; a--)
                {
                    Debug.LogError(sortingArray[a][0] + " " + sortingArray[a][1]);
                    sortingArray[a + 1] = sortingArray[a];
                }
                sortingArray[i] = newLine;
            }        
        }

        // Use this to rewrite file
        Debug.Log(sortingArray.Length);
        reader.Close();
        using (StreamWriter writer = new StreamWriter(directory + textDir))
        {
            string output = string.Empty;
            string outputL = string.Empty;
            //for (int i = 0; i < sortingArray.Length; i++)
            //{
            //    output = $"{sortingArray[i][0]}:{sortingArray[i][1]}";
            //    if(i<=10)
            //        outputL += $"{output}\n";
            //}

            // TEMP (this copies the loop)
            int b = 1;
            while (b < sortingArray.Length)
            {
                output = $"{sortingArray[b][0]}:{sortingArray[b][1]}";
                if (b <= 10)
                    outputL += $"{output}\n";
                b++;
            }
            leaderBox.text = outputL;
            writer.WriteLine(output);
            writer.Close();
        }
    }
    public void ReadScores()
    {
        reader = new StreamReader(directory + textDir);
        string outputL = string.Empty;

        int b = 0;
        while(b<10)
        {
            Debug.Log("Loop");

            string[] a = reader.ReadLine().Split(" ");
            outputL += $"{a[0]} : {a[1]}\n";

            Debug.Log(outputL);
            b++;
        }
        leaderBox.text = outputL;
    }
}
