using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectClick : MonoBehaviour, IPointerDownHandler
{
    public Dialogue textBox;
    public DialogueClass thisDialogue;
    public List<List<string>> dialogueGrid;
    public CapsuleCollider player;

    private void Start()
    {
        AddPhysicsRaycaster();
    }

    //Raycaster checks what gameobject is clicked
    private void AddPhysicsRaycaster()
    {
        PhysicsRaycaster physicsRaycaster = FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            //add to cam and add to physicsRaycaster variable
            physicsRaycaster = Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
        if (eventData.pointerCurrentRaycast.gameObject.CompareTag("CanSpeak"))
        {
            // Send next dialogue to be read - loop dialogue when other options deplete
            textBox.StartDialogue(dialogueGrid[thisDialogue.customIndex], player, this);

            // adds to unique NPC text index (basically "has spoken this line")
            // all of this is handled according to each prefab
            thisDialogue.customIndex += 1;
            if (thisDialogue.customIndex > thisDialogue.maxIndex)
                thisDialogue.customIndex = thisDialogue.maxIndex;
        }
    }
}