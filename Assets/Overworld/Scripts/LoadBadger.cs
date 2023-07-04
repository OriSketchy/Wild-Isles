using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Networking.PlayerConnection;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadBadger : MonoBehaviour
{
    public GameObject player;
    public FollowPlayer playerCam;
    public Transform tempTarget;
    public BatlleSystem battleBadger;
    public GameObject battleUI;

    public TextMeshProUGUI counterCurrent;
    public TextMeshProUGUI counterTotal;

    private int enemiesDefeated;
    private int enemiesTotal;

    private CapsuleCollider playerCollider;

    //REMOVE BEFORE BUILD
    public MeshRenderer border;

    private void Start()
    {
        battleUI.SetActive(false);
        playerCollider = player.GetComponent<CapsuleCollider>();

        //REMOVE BEFORE BUILD
        border.enabled = false;
    }
    public void UpdateCounter(int increaseTotal = 0)
    {
        enemiesTotal += increaseTotal;

        counterCurrent.text = $"{enemiesDefeated}";
        counterTotal.text = $"{enemiesTotal}";
    }
    public IEnumerator BattleEntry(GameObject enemyEngaged, Vector3 midpoint, Transform angle)
    {
        // Be called when player collides with enemy
        // paralyse them & remove player collider
        player.GetComponent<WASDMovement>().enabled = false;
        enemyEngaged.GetComponent<EnemyMovement>().enabled = false;
        playerCollider.enabled = false;

        // make them face each other
        player.transform.localScale = new Vector3(-1, 1, 1);
        enemyEngaged.transform.localScale = new Vector3(1, 1, 1);

        // Halt for a second 
        yield return new WaitForSeconds(1);

        // New way - camera is parented to an object that follows player, so the axis is offset. This overrides that empty's following script
        tempTarget.gameObject.SetActive(true);
        playerCam.target = tempTarget;

        // using enemy rotation, offset by 90 degrees so Duffin is on the left no matter what
        playerCam.transform.Rotate(new Vector3(0, angle.eulerAngles.y + 90, 0)); ;
        tempTarget.position = midpoint;
        angle.parent = enemyEngaged.transform;

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
        playerCam.transform.rotation = new Quaternion(0, 0, 0, 0);

        tempTarget.gameObject.SetActive(false);

        // Disable HUD, BB and Enemy
        battleBadger.playerUnit = null;
        battleBadger.enemyUnit = null;
        battleBadger.gameObject.SetActive(false);
        battleUI.SetActive(false);

        if(battleBadger.state == BattleState.LOST)
        {
            // ends battle to fail screen
            StartCoroutine(GameEnd(false));
        }
        else if(battleBadger.state == BattleState.WON)
        {
            // Restore Duffin (BattleBadger manages if enemy is alive)
            player.GetComponent<WASDMovement>().enabled = true;
            playerCollider.enabled = true;
            // Counter handling
            enemiesDefeated++;
            UpdateCounter();
        }
        yield break;
    }
    public IEnumerator GameEnd(bool victory)
    {
        PlayerPrefs.SetInt("score", player.GetComponent<Unit>().unitXP);
        if (victory)
        {
            SceneManager.LoadScene("MenuWon");
        }
        else
        {
            SceneManager.LoadScene("MenuFail");
        }
        yield break;
    }
}