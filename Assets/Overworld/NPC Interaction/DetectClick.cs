using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectClick : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    Dialogue dialogue;

    private void Start()
    {
        AddPhysicsRaycaster();
    }

    //Rasycaster checks what gameobject is clicked
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
            Debug.Log("Script works");
            dialogue.StartDialogue();
        }
    }
}