using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// List of battle states
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BatlleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    // SETUP VARIABLES TO GET INVENTORY DATA
    
    // change how this works pretty please
    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    // figure out changing this to a textmeshpro please
    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Transform UIParent;

    public BattleState state;
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    // BATTLE START STATE
    IEnumerator SetupBattle()
    {
        // instantiates are temporary. getcomponent is forever.
        GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGo.GetComponent<Unit>();

        GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGo.GetComponent<Unit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        // unique opening text depending on enemy type
        // This may change
        switch (enemyUnit.unitID)
        {
            case 0:
                dialogueText.text = "how in GOD'S NAME did you end up here. Something has gone wrong.";
                break;
            case 1:
                dialogueText.text = "A plastic bag rolls meekly towards you. It appears to have little eyes?";
                break;
            case 2:
                dialogueText.text = "A pile of cakebox scraps shifts into what appears to be a frog. It looks at you, hungrily.";
                break;
            case 3:
                dialogueText.text = "Whenver this slime shifts it sounds like wet sand. It smells like tuna";
                break;
            case 4:
                dialogueText.text = "The dragon seems to be looking at you. It reeks of rotting plastic. It must be old.";
                break;
        }
        // delaying coroutine so player gets a chance to read. May make this wait skippable with input
        yield return new WaitForSeconds(2f);

        // moves to next scene
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    // BATTLE PLAYER TURN STATE
    void PlayerTurn()
    {
        dialogueText.text = "Choose your action!";
    }
    IEnumerator PlayerAttack(int damType)
    {
        // damages the enemy and checks if dead
        bool isDead = enemyUnit.TakeDamage(playerUnit.damageBase, damType);
        // updates enemy hp and throws dialogue
        enemyHUD.SetHP(enemyUnit.currentHP);
        dialogueText.text = "You strike the enemy!";
        // end of turn
        // yield return is here so player can't spam click buttons
        if(isDead) 
        {
            state = BattleState.WON;
            yield return new WaitForSeconds(2f);
            EndBattle();
        } 
        else 
        {
            state = BattleState.ENEMYTURN;
            yield return new WaitForSeconds(2f);
            StartCoroutine(EnemyTurn());
        } 
    }
    IEnumerator PlayerHeal()
    {
        // placeholder amount - change to inventory item
        playerUnit.HealUnt(5);
        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You restore a little health. Just hold out a little longer";
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    public void OnItemButton()
    {
        // heals by default. tweak to be more specific
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerHeal());
    }
    // BATTLE ENEMY TURN STATE
    IEnumerator EnemyTurn()
    {
        // same pattern as player turn, just automatically attacks
        dialogueText.text = enemyUnit.unitName + " attacks you.";
        yield return new WaitForSeconds(1f);
        bool isDead = playerUnit.TakeDamage(enemyUnit.damageBase, 4);
        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);
        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    // BATTLE WIN/LOSE STATE
    void EndBattle()
    {
        // add battle rewards and save data to the Loadbadger
        if(state == BattleState.WON) { dialogueText.text = "You win the battle"; }
        // this straight up kills the bird idk if we can bring him back 
        // remember to make a game over scene please
        else if(state == BattleState.LOST) { dialogueText.text = "You are defeated!"; }
    }

    // UI CONTROLS VV
    void ResetButtons()
    {
        foreach( Transform child in UIParent ) { child.gameObject.SetActive(false); }
        UIParent.GetChild(0).gameObject.SetActive(true);
        UIParent.GetChild(1).gameObject.SetActive(true);
    } // use this for "back" buttons
    public void OnAttackButton(int damType)
    {
        // attacks by default. will be a choice
        if (state != BattleState.PLAYERTURN)
            return;
        StartCoroutine(PlayerAttack(damType));
    }
    public void onScrapButon(int dmgType)
    {

    }
}