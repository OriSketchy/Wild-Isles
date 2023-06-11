using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDMovement : MonoBehaviour
{
    // public GameObject character;
    
    // [SerializeField] 
    // [Range(0, 10)] private float speed;
    
    // [SerializeField] 
    // private float horizontalRange = 6f;
    
    // [SerializeField] 
    // private float verticalRange = 6f;
    
    // void Update()
    // {
    //     //Get unity's built in Horizontal and Vertical definitions
    //     float horizontalMove = Input.GetAxis("Horizontal");
    //     float verticalMove = Input.GetAxis("Vertical");

    //     //Move at the speed defined in inspector
    //     float horizontalOffset = horizontalMove * speed * Time.deltaTime;
    //     float verticalOffset = verticalMove * speed * Time.deltaTime;

    //     //Clamp horizontal movement
    //     float rawHorizontalPos = transform.position.x + horizontalOffset;
    //     float clampHorizontalPos = Mathf.Clamp(rawHorizontalPos, -horizontalRange, horizontalRange);
        
    //     //Clamp vertical movement
    //     float rawVerticalPos = transform.position.z + verticalOffset;
    //     float clampVerticalPos = Mathf.Clamp(rawVerticalPos, -verticalRange, verticalRange);

    //     //Move
    //     transform.position = new Vector3(clampHorizontalPos, transform.position.y, clampVerticalPos);
    // }



 public GameObject character;
    
    [SerializeField] 
    [Range(0f, 1f)] private float speed;
    public Vector3 centerPt;
    public float radius;

    void Update()
    {
        //get all inputs, multiply them by eachother to get the direction, normalise, then multipy by movementSpeed
        
        // Get the new position for the object.
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 newPos = transform.position + movement;

        // Calculate the distance of the new position from the center point. Keep the direction
        // the same but clamp the length to the specified radius.
        Vector3 offset = newPos - centerPt;
        transform.position = centerPt + Vector3.ClampMagnitude(offset, radius);

    }




    // void Update() 
    // {    
    //     Move();
    //     ClampSpeed();
    // }

    // public void Move()
    // {
    //         if (Input.GetKey("d"))
    //         {
    //             transform.position += Vector3.right * speed * Time.deltaTime;
    //         }
    //         if (Input.GetKey("a"))
    //         {
    //             transform.position += Vector3.left* speed * Time.deltaTime;
    //         }
    //         if (Input.GetKey("w"))
    //         {
    //             transform.position += Vector3.forward * speed * Time.deltaTime;
    //         }
    //         if (Input.GetKey("s"))
    //         {
    //             transform.position += Vector3.back * speed * Time.deltaTime;
    //         }
    // }

    // public void ClampSpeed()
    // {
    //     if ()
    // }

    
}
