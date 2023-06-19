using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WASDMovement : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    [Range(0, 10)] public float speed;

    public LoadBadger theBadger;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        //vector is automatically set to 0, 0, 0 so player doesnt move without input
        Vector3 direction = Vector3.zero;

        //Add up player input
        if (Input.GetKey("d"))
        {
            direction += Vector3.right;
        }
        if (Input.GetKey("a"))
        {   
            direction += Vector3.left;
        }
        if (Input.GetKey("w"))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey("s"))
        {
            direction += Vector3.back;
        }

        //disable movement if more than 2 keys pressed
        //there is probably a better way to do it, i'll hunt through the unity scripting API later
        if(Input.GetKey("w") && Input.GetKey("s") && Input.GetKey("d")  ||  Input.GetKey("w")  && Input.GetKey("s") && Input.GetKey("a")  ||  Input.GetKey("w")  && Input.GetKey("d") && Input.GetKey("a")  ||  Input.GetKey("d")  && Input.GetKey("s") && Input.GetKey("a"))
        {
            direction = Vector3.zero;
        }
        
        //Execute player input
        transform.position += direction.normalized * speed * Time.fixedDeltaTime; 
        
        //if getting any WASD input play walk animation
        if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
        {
            animator.SetBool("Walk", true);
        }

        //if either not getting any  WASD input, or getting conflicted input play idle anim.
        //There is definitely a better way to do this, possibly getting the velocity of the player and if they're above a certain speed enable walk anim?
        if(!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d")    ||     Input.GetKey("w") && Input.GetKey("s") || Input.GetKey("a")  && Input.GetKey("d")    ||     Input.GetKey("w") && Input.GetKey("s") && Input.GetKey("d") || Input.GetKey("w")  && Input.GetKey("s") && Input.GetKey("a")    ||    Input.GetKey("w")  && Input.GetKey("d") && Input.GetKey("a")  ||  Input.GetKey("d")  && Input.GetKey("s") && Input.GetKey("a"))
        {
            animator.SetBool("Walk", false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // check if player is actually colliding with enemy. Or if even is Duffin
        if (!other.CompareTag("Enemy") || this.CompareTag("Enemy"))
            return;

        this.transform.position += Vector3.left * 5f;

        // Get all enemy data, angle of player to enemy, and midpoint of the two. Coroutine will handle the rest
        GameObject enemy = other.gameObject;
        Transform playerAngle = this.transform.GetChild(1).gameObject.GetComponent<LookAtEnemy>().Stare(enemy);
        Vector3 midpoint = (enemy.transform.position + this.transform.position) * 0.5f;

        animator.SetBool("Walk", false);

        StartCoroutine(theBadger.BattleEntry(enemy, midpoint, playerAngle));
    }
}
