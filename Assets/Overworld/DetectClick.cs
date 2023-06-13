using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class DetectClick : MonoBehaviour, IPointerDownHandler
{

    //good luck
    private void Start()
    {
        AddPhysicsRaycaster();
        
    }

    private void AddPhysicsRaycaster()
    {
        PhysicsRaycaster physicsRaycaster = FindObjectOfType<PhysicsRaycaster>();
        if (physicsRaycaster == null)
        {
            Camera.main.gameObject.AddComponent<PhysicsRaycaster>();
        }
        
        if (physicsRaycaster.CompareTag("CanSpeak"))
        {
            Debug.Log("Youre did it");
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + eventData.pointerCurrentRaycast.gameObject.name);
    }

   
    


    // void FixedUpdate()
    // {
    //     if (PhysicsRaycaster.CompareTag("CanSpeak") != null)
    //     {
    //         Debug.Log(hit.transform.gameObject.name);
    //     }
    // }
}
