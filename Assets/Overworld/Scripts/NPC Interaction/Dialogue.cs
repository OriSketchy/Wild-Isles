using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public List<string> lines;
    [Range(0f, 0.1f)] public float textSpeed;  //the lower the textSpeed the faster text moves
    //[Range(0f, 2f)] public float mouseCooldown;
    
    private int index;
    private DialogueClass currentNPC;

    void Start()
    {
         textComponent.text = string.Empty;
    }

    void Awake()
    {
        gameObject.SetActive(false);
    }

    void Update()
     {
         if(Input.GetMouseButtonDown(0))
         {
             if (textComponent.text == lines[index])
             {
                 NextLine();
             }
             else
             {
                 StopAllCoroutines();
                 textComponent.text = lines[index];
             }
         }
     }

     public void StartDialogue(DialogueClass dialogueData, List<string> dialogueOptions)
     {
        currentNPC = dialogueData;
        lines = dialogueOptions;
        gameObject.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
     }

     IEnumerator TypeLine()
     {
         foreach (char c in lines[index].ToCharArray())
         {
             textComponent.text += c;
             yield return new WaitForSeconds(textSpeed);
         }
     }

     void NextLine()
     {
         if(index < lines.Count - 1)
         {
             index++;
             textComponent.text = string.Empty;
             StartCoroutine(TypeLine());
         }
         else
         {
            textComponent.text = string.Empty;
            gameObject.SetActive(false);
         }
     }
 }