using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;

public class LoadBadger : MonoBehaviour
{
    public GameObject player;
    public FollowPlayer playerCam;
    public GameObject tempTarget;
    public BatlleSystem battleBadger;
    public GameObject battleUI;

    private void Start()
    {
        battleUI.active = false;
    }

    public IEnumerator BattleEntry(GameObject enemyEngaged, Vector3 midpoint, Transform playerAngle)
    {
        // Be called when player collides with enemy
        // paralyse them
        player.GetComponent<WASDMovement>().enabled = false;
        //enemyEngaged.GetComponent<WASDMovement>().enabled = false;
        // Halt for a second 
        yield return new WaitForSeconds(1);
        // Set camera target as empty, then move empty to midpoint. Remove empty upon battle completion
        tempTarget.SetActive(true);
        tempTarget.transform.position = midpoint;
        playerCam.target = tempTarget.transform;
        // Load battle data
        battleUI.SetActive(true);
        // Enable BattleBadger & send data to him
        battleBadger.playerUnit = player.GetComponent<Unit>();
        battleBadger.enemyUnit = enemyEngaged.GetComponent<Unit>();
        battleBadger.gameObject.SetActive(true);
    }
    public IEnumerator BattleExit()
    {
        // Return camera target to player
        playerCam.target = player.transform;
        tempTarget.SetActive(false);
        // Disable HUD, BB and Enemy
        battleUI.SetActive(false);
        battleBadger.playerUnit = null;
        battleBadger.enemyUnit = null;
        battleBadger.gameObject.SetActive(false);
        // Halt for a second
        //yield return new WaitForSeconds(1);
        // ^^^^^ DON'T TOUCH THIS LINE WE'LL FIX IT LATER ^^^^^

        // Un-paralyse Duffin
        player.GetComponent<WASDMovement>().enabled = true;
        Debug.Log("oarugh");
        yield break;
    }
}
