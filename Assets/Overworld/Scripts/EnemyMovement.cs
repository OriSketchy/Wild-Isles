using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

// CHANGE:
// - Inputs to variables
// - Animations to enemy animation sets
// - Basic enemy movement AI (set unique stats for each enemy)
//  - Chance to move toward player


public class EnemyMovement : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    [Range(0, 10)] public float speed;

    LoadBadger theBadger;
    public GameObject player;

    private SphereCollider myCollider;
    [SerializeField] private Transform eyes;

    // For counting active inputs
    int inputNum = 0;

    // 0A, 1D, 2W, 3S, the rest are faux
    // Used in place of player input
    private List<bool> movement = new List<bool> { false, false, false, false, false, false, false };

    private void Awake()
    {
        theBadger = FindFirstObjectByType<LoadBadger>();
        animator = GetComponentInChildren<Animator>();
        myCollider = this.GetComponent<SphereCollider>();
        StartCoroutine(RandMove());
    }

    void FixedUpdate()
    {
        //vector is automatically set to 0, 0, 0 so player doesnt move without input
        Vector3 direction = Vector3.zero;
        inputNum = 0;

        //Add up player input
        if (movement[1])
        {
            direction += Vector3.right;
            inputNum += 1;

            // TEMP FIX
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (movement[0])
        {   
            direction += Vector3.left;
            inputNum += 1;

            // TEMP FIX
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        if (movement[2])
        {
            direction += Vector3.forward;
            inputNum += 1;
        }
        if (movement[3])
        {
            direction += Vector3.back;
            inputNum += 1;
        }

        //disable movement if more than 2 keys pressed
        //there is probably a better way to do it, i'll hunt through the unity scripting API later
        if(inputNum > 2)
        {
            direction = Vector3.zero;
        }
        
        //Execute player input
        transform.position += direction.normalized * speed * Time.fixedDeltaTime; 
        
        //if getting any WASD input play walk animation
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            // FIX
            //animator.SetBool("Walk", true);
        }

        //if either not getting any  WASD input, or getting conflicted input play idle anim.
        //There is definitely a better way to do this, possibly getting the velocity of the player and if they're above a certain speed enable walk anim?
        if(!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d")    ||     Input.GetKey("w") && Input.GetKey("s") || Input.GetKey("a")  && Input.GetKey("d")    ||     Input.GetKey("w") && Input.GetKey("s") && Input.GetKey("d") || Input.GetKey("w")  && Input.GetKey("s") && Input.GetKey("a")    ||    Input.GetKey("w")  && Input.GetKey("d") && Input.GetKey("a")  ||  Input.GetKey("d")  && Input.GetKey("s") && Input.GetKey("a"))
        {
            //animator.SetBool("Walk", false);
        }
    }

    IEnumerator RandMove()
    {
        movement[Random.Range(0, 4)] = true;
        movement[Random.Range(0, 6)] = true;
        yield return new WaitForSeconds((Random.Range(0, 200)/100));
        movement = new List<bool> { false, false, false, false, false, false, false };
        
        StartCoroutine(RandMove());
        yield break;
    }

    // Used on flee only
    // prevent player from re-entering battle by both disabling collider and moving away from enemy
    public void InitiateCooldown()
    {
        StartCoroutine(Cooldown());
    }
    public IEnumerator Cooldown()
    {
        myCollider.enabled = false;
        yield return new WaitForSeconds(5);
        myCollider.enabled = true;
        yield break;
    }

    // idk how else to do this ok
    public Transform Stare(Transform enemy)
    {
        // looks at player and returns as a transform
        eyes.parent = null;
        eyes.LookAt(enemy);
        return eyes;
    }
}