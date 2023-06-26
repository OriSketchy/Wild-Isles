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
    
    private int index = 0;
    private DialogueClass currentNPC;
    private CapsuleCollider playerCollider;
    private DetectClick selectedNPC;

    void Start()
    {
        // Clears box
        textComponent.text = string.Empty;
    }

    //void Awake()
    //{
    //    gameObject.SetActive(false);
    //}

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

     public void StartDialogue(DialogueClass dialogueData, List<string> dialogueOptions, CapsuleCollider player, DetectClick NPC)
     {
        playerCollider = player;
        selectedNPC = NPC;
        // Disables player collider, sets data from clicked NPC, then starts running through the selected array of lines
        playerCollider.enabled = false;
        // Disables player movement and the ability to re-click on Davide
        playerCollider.gameObject.GetComponent<WASDMovement>().enabled = false;
        selectedNPC.enabled = false;
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
            playerCollider.enabled = true;
            playerCollider.gameObject.GetComponent<WASDMovement>().enabled = true;
            selectedNPC.enabled = true;
        }
     }
 }