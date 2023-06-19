using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// List of battle states
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, FLEE }

public class BatlleSystem : MonoBehaviour
{
    //public GameObject playerUnit;
    //public GameObject enemyPrefab;
    // SETUP VARIABLES TO GET INVENTORY DATA

    // change how this works pretty please
    //public Transform playerBattleStation;
    //public Transform enemyBattleStation;
    public LoadBadger theBadger;

    public Unit playerUnit;
    public Unit enemyUnit;

    // figure out changing this to a textmeshpro please
    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Transform UIParent;

    public BattleState state;
    private int turnCount;
    void OnEnable()
    {
        turnCount = 0;
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    // BATTLE START STATE
    IEnumerator SetupBattle()
    {
        // instantiates are temporary. getcomponent is forever.
        //GameObject playerGo = Instantiate(playerPrefab, playerBattleStation);
        //playerUnit = playerGo.GetComponent<Unit>();

        //GameObject enemyGo = Instantiate(enemyPrefab, enemyBattleStation);
        //enemyUnit = enemyGo.GetComponent<Unit>();

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        ResetButtons();

        // Dialogue text is placeholder
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
    IEnumerator PlayerAttack(int damType, int damMod = 0)
    {
        // damages the enemy and checks if dead
        bool isDead = enemyUnit.TakeDamage(playerUnit.damageBase, damType, damMod);
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
    IEnumerator PlayerHeal(int itemSlot)
    {
        // placeholder amount - change to inventory item
        playerUnit.HealUnit(itemSlot);
        playerHUD.SetHP(playerUnit.currentHP);
        yield return new WaitForSeconds(2f);
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }
    // BATTLE ENEMY TURN STATE
    IEnumerator EnemyTurn()
    {
        // same pattern as player turn, just automatically attacks
        dialogueText.text = enemyUnit.unitName + " attacks you.";
        yield return new WaitForSeconds(1f);
        enemyUnit.currentSP -= 5;
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
        if(state == BattleState.WON) 
        { 
            dialogueText.text = "You win the battle";
            // calculate XP gain
            playerUnit.unitXP += 100 - (10 * turnCount);
            // calculate SP gain
            playerUnit.currentSP += enemyUnit.currentSP;
            if (playerUnit.currentSP > playerUnit.maxSP)
                playerUnit.maxSP = playerUnit.currentSP;

            Destroy(enemyUnit.gameObject);

            StartCoroutine(theBadger.BattleExit());
        }
        // this straight up kills the bird idk if we can bring him back 
        // remember to make a game over scene please
        else if(state == BattleState.LOST) 
        {
            dialogueText.text = "You are defeated!";

            Destroy(playerUnit.gameObject);

            StartCoroutine(theBadger.BattleExit());
        }
        // skip XP gain, reset HP and keep SP loss to soft punish fleeing
        else if(state == BattleState.FLEE) 
        {
            dialogueText.text = "You flee the battle";
            enemyUnit.currentHP = enemyUnit.maxHP;
            StartCoroutine(theBadger.BattleExit());
            //enemyUnit.GetComponent<WASDMovement>().enabled = true;
        }
    }


    // UI CONTROLS VV
    public void ResetButtons()
    {
        // use this for "back" buttons
        foreach ( Transform child in UIParent ) { child.gameObject.SetActive(false); }
        for(int i = 0; i < 3; i++) { UIParent.GetChild(i).gameObject.SetActive(true); }
    } 
    public void OnAttackParent() 
    {
        UIParent.GetChild(1).gameObject.SetActive(false);
        UIParent.GetChild(3).gameObject.SetActive(true);
    }
    public void OnItemParent() 
    {
        UIParent.GetChild(1).gameObject.SetActive(false);
        GameObject itemButtons = UIParent.GetChild(4).gameObject;
        itemButtons.SetActive(true);

        // check each item is present. Disable if not
        // assigning data to each button will be done externally
        // okay actualy do ALL of this externally just enable the chiildren upon item pickup good lord
        for(int i = 0; i < 3; i++) 
        {
            if (playerUnit.itemConsumes[i] != null) 
            { 
                itemButtons.transform.GetChild(i+2).gameObject.SetActive(true); 
            } // jesus christ there has to be a better way
            else 
            { 
                itemButtons.transform.GetChild(i+2).gameObject.SetActive(false);
            }
        }
    }
    // For now the SP attacks will share the same damage bonus
    public void OnAttackButton(int damType)
    {
        // different buttons input different attack types
        // SP buttons will do damage based on current selected weapon (set defaults for demo)
        if (state != BattleState.PLAYERTURN)
            return;
        ResetButtons();

        if (damType <= 3) { StartCoroutine(PlayerAttack(damType)); }
        else if(damType > 3) 
        {
            // Dialogue text is placeholder
            if(playerUnit.currentSP < 10) { dialogueText.text = "You don't have enough SP for that!"; }
            else if(playerUnit.items[damType - 4]) { StartCoroutine(PlayerAttack(damType, 15)); }
            else { dialogueText.text = "You don't have the right weapon for that!"; }

            playerUnit.currentSP -= 10;
        }
    }
    public void OnItemButton(int itemSlot)
    {
        if (state != BattleState.PLAYERTURN)
            return;

        // dialogue text is placeholder
        dialogueText.text = $"You eat your {playerUnit.itemConsumes[itemSlot].name} and restore HP!";
        StartCoroutine(PlayerHeal(itemSlot));
    }
    public void OnFleeButton() 
    {
        state = BattleState.FLEE;
        // end battle event. Skip stat gains and reset enemy HP, keep lowered SP (soft punish for fleeing)
        EndBattle();
    }
}