using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class WASDMovement : MonoBehaviour
{
    Animator animator;
    [SerializeField]
    [Range(0, 10)] public float speed;

    public LoadBadger theBadger;
    public Unit self;

    public Canvas UIParent;
    public Dialogue textBox;

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

            // TEMP FIX
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        if (Input.GetKey("a"))
        {   
            direction += Vector3.left;

            // TEMP FIX
            this.transform.localScale = new Vector3(1, 1, 1);
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
            animator.SetBool("DuffinWalk", true);
        }

        //if either not getting any  WASD input, or getting conflicted input play idle anim.
        //There is definitely a better way to do this, possibly getting the velocity of the player and if they're above a certain speed enable walk anim?
        if(!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d") ||
            Input.GetKey("w") && Input.GetKey("s") ||
            Input.GetKey("a") && Input.GetKey("d") ||
            Input.GetKey("w") && Input.GetKey("s") && Input.GetKey("d") ||
            Input.GetKey("w") && Input.GetKey("s") && Input.GetKey("a") ||
            Input.GetKey("w") && Input.GetKey("d") && Input.GetKey("a") ||
            Input.GetKey("d") && Input.GetKey("s") && Input.GetKey("a"))
        {
            animator.SetBool("DuffinWalk", false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // Give enemies a unique movement script
        if (this.CompareTag("Enemy"))
            return;
        if (other.CompareTag("Enemy"))
        {
            // Get all enemy data, angle of player to enemy, and midpoint of the two. Coroutine will handle the rest
            Transform enemy = other.transform;
            enemy.position = Vector3.MoveTowards(enemy.position, this.transform.position, -5);

            Transform angle = enemy.GetComponent<EnemyMovement>().Stare(this.transform);

            Vector3 midpoint = (enemy.position + this.transform.position) * 0.5f;

            animator.SetBool("DuffinWalk", false);

            StartCoroutine(theBadger.BattleEntry(enemy.gameObject, midpoint, angle));
        }
        else if (other.CompareTag("AsleepEnemy"))
        {
            // Awaken enemy movement
            other.GetComponent<EnemyMovement>().enabled = true;
            // Change tag
            other.tag = "Enemy";
            // Resize collider
            other.GetComponent<SphereCollider>().radius = 1.1f;
        }
        else if (other.CompareTag("PickupConsume"))
        {
            // add to rudimentary inventory, then disables its overworld asset
            var item = other.GetComponent<ItemPickup>();
            self.itemConsumes.Add(item);
            other.gameObject.SetActive(false);
            //UIParent.BroadcastMessage("RefreshButton");
            // send dialogue message
            textBox.StartDialoguePickup(item.info, this.GetComponent<CapsuleCollider>());
        }
        else if (other.CompareTag("PickupWeapon"))
        {
            WeaponPickup weapon = other.GetComponent<WeaponPickup>();
            self.items[weapon.damageType-1] = weapon;
            other.gameObject.SetActive(false);
            //UIParent.BroadcastMessage("RefreshButton");
            textBox.StartDialoguePickup(weapon.info, this.GetComponent<CapsuleCollider>());
        }
        else if (other.CompareTag("SpawnRegion"))
        {
            spawnRegion region = other.GetComponent<spawnRegion>();
            region.Spawn();
        }
    }
}
