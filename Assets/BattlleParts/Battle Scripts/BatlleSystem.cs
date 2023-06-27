using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

// List of battle states
public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, FLEE }

public class BatlleSystem : MonoBehaviour
{
    public LoadBadger theBadger;

    public Unit playerUnit;
    public Unit enemyUnit;

    // figure out changing this to a textmeshpro please
    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public Transform UIParent;
    public GameObject tempTarget;
    public Camera mainCamera;

    public BattleState state;
    private int turnCount;
    void OnEnable()
    {
        turnCount = 0;
        state = BattleState.START;
        ResetButtons();
        StartCoroutine(SetupBattle());
    }

    // BATTLE START STATE
    IEnumerator SetupBattle()
    {
        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);
        ResetButtons();
        UIParent.BroadcastMessage("ButtonDisable");

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
            case 100:
                dialogueText.text = "You stand upon the grave of God and weep, for the infernal bear is here.";
                break;
            case 69:
                dialogueText.text = "Somehow, you have engaged the God of your land. He's displeased. Good luck.";
                break;
        }
        // delaying coroutine so player gets a chance to read. May make this wait skippable with input
        yield return new WaitForSeconds(3f);

        // moves to next scene
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    // BATTLE PLAYER TURN STATE
    void PlayerTurn()
    {
        UIParent.BroadcastMessage("ButtonEnable");
        state = BattleState.PLAYERTURN;
        dialogueText.text = "Choose your action!";
    }
    IEnumerator PlayerAttack(int damType, int damMod = 0)
    {
        UIParent.BroadcastMessage("ButtonDisable");
        // damages the enemy and checks if dead
        bool isDead = enemyUnit.TakeDamage(playerUnit.damageBase, damType, damMod);
        // updates enemy hp and throws dialogue
        enemyHUD.SetHP(enemyUnit.currentHP);
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
        UIParent.BroadcastMessage("ButtonDisable");
        // placeholder amount - change to inventory item
        playerUnit.HealUnit(itemSlot);
        playerHUD.SetHP(playerUnit.currentHP);
        playerUnit.itemConsumes.RemoveAt(itemSlot);
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
            playerUnit.unitXP += 100 - (Random.Range(3,10) * turnCount);
            // calculate SP gain
            playerUnit.currentSP += enemyUnit.currentSP;
            if (playerUnit.currentSP > playerUnit.maxSP)
                playerUnit.maxSP = playerUnit.currentSP;

            // small scene if enemy has a drop
            if (enemyUnit.items.Count > 0)
            {
                // weapon drop cutscene
                StartCoroutine(WeaponPickup());
            }
            else if (enemyUnit.itemConsumes.Count > 0)
            {
                // item drop cutscene
                StartCoroutine(ConsumePickup());
            }
            else
            {
                // regular enemy
                Destroy(enemyUnit.gameObject);

                StartCoroutine(theBadger.BattleExit());
            }
        }
        // this straight up kills the bird idk if we can bring him back 
        // remember to make a game over scene please
        else if(state == BattleState.LOST) 
        {
            dialogueText.text = "You are defeated.";

            Destroy(playerUnit.gameObject);

            StartCoroutine(theBadger.BattleExit());
        }
        // skip XP gain, reset HP and keep SP loss to soft punish fleeing
        else if(state == BattleState.FLEE) 
        {
            dialogueText.text = "You flee the battle";
            enemyUnit.currentHP = enemyUnit.maxHP;
            // Prevents player from re-entering battle immediately
            enemyUnit.GetComponent<EnemyMovement>().InitiateCooldown();
            enemyUnit.GetComponent<EnemyMovement>().enabled = true;
            StartCoroutine(theBadger.BattleExit());
        }
    }


    // UI CONTROLS VV
    public void ResetButtons()
    {
        // use this for "back" buttons
        // silly little function
        StartCoroutine(ResetsButtons());
    }
    public IEnumerator ResetsButtons()
    {
        foreach (Transform child in UIParent) { child.gameObject.SetActive(false); }
        for (int i = 0; i < 3; i++) { UIParent.GetChild(i).gameObject.SetActive(true); }
        dialogueText.text = "Choose your action!";
        yield break;
    }
    // Remember the battle state will be changed in the damage/heal functions
    public void OnAttackParent()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        UIParent.GetChild(1).gameObject.SetActive(false);
        UIParent.GetChild(3).gameObject.SetActive(true);
    }
    public void OnItemParent() 
    {
        if (state != BattleState.PLAYERTURN)
            return;

        UIParent.GetChild(1).gameObject.SetActive(false);
        GameObject itemButtons = UIParent.GetChild(4).gameObject;
        itemButtons.SetActive(true);
    }
    public void OnAttackButton(int damType)
    {
        // different buttons input different attack types
        // SP buttons will do damage based on current selected weapon (set defaults for demo)
        ResetButtons();
        // For now the SP attacks will share the same damage bonus
        if (damType <= 3) { StartCoroutine(PlayerAttack(damType)); }
        else if(damType > 3) 
        {
            // Dialogue text is placeholder
            if(playerUnit.currentSP < 10) 
            {
                dialogueText.text = "You don't have enough SP for that!";
            }
            else if(playerUnit.items[damType - 4] != null)
            {
                dialogueText.text = $"You use 10 Scrap to attack the enemy with {playerUnit.items[damType - 4].name}!";
                StartCoroutine(PlayerAttack(playerUnit.items[damType-4].damageMod,
                    playerUnit.items[damType - 4].damageType));
            }
            else 
            {
                // this shouldn't happen. dont ask why it's still here.
                dialogueText.text = "You don't have the right weapon for that! also how did you click this this is debugged now what";
            }

            playerUnit.currentSP -= 10;
        }
    }
    public void OnItemButton(int itemSlot)
    {
        ResetButtons();
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

    // MINI CUTSCENES
    IEnumerator WeaponPickup()
    {
        //var damType = "DAMTYPE";

        //if (enemyUnit.items[0].damageType.ToString() == "1")
        //    damType = "Stab";
        //else if (enemyUnit.items[0].damageType.ToString() == "2")
        //    damType = "Grab";
        //else if (enemyUnit.items[0].damageType.ToString() == "3")
        //    damType = "Thwab";

        // HOLD DEATH ANIMATION
        dialogueText.text = $"Enemy is defe- What's this?";
        yield return new WaitForSeconds(2f);
        // move and zoom camera (standard is 60)
        // FIGURE OUT HOW TO INTERPOLATE THIS
        tempTarget.transform.position = enemyUnit.transform.position;
        mainCamera.fieldOfView = 20;
        // ENEMY HIDES HERE (sprite replaced with item sprite)
        // CHANGE THIS TO SUIT ANIMATOR
        enemyUnit.gameObject.SetActive(false);
        Instantiate(enemyUnit.items[0].gameObject, new Vector3(enemyUnit.transform.position.x, 1, enemyUnit.transform.position.z), Quaternion.identity);
        dialogueText.text = $"This enemy contained a Weapon!";
        yield return new WaitForSeconds(1.5f);
        dialogueText.text = $"A {enemyUnit.items[0].itemName} was inside the fallen {enemyUnit.unitName}";
        yield return new WaitForSeconds(1.5f);
        Destroy(enemyUnit.gameObject);

        // camera is mostly restored elsewhere. Unzoom here
        mainCamera.fieldOfView = 60;

        StartCoroutine(theBadger.BattleExit());
        //yield break;
    }
    IEnumerator ConsumePickup()
    {
        // Consume drop cutscene
        // HOLD DEATH ANIMATION
        dialogueText.text = $"You slayed the enem- What's this?";
        yield return new WaitForSeconds(1f);
        // move and zoom camera (standard is 60)
        // FIGURE OUT HOW TO INTERPOLATE THIS
        tempTarget.transform.position = enemyUnit.transform.position;
        mainCamera.fieldOfView = 20;
        // ENEMY HIDES HERE (sprite replaced with item sprite)
        // CHANGE THIS TO SUIT ANIMATOR
        enemyUnit.gameObject.SetActive(false);
        dialogueText.text = $"This enemy contained an item!";
        Instantiate(enemyUnit.itemConsumes[0].gameObject, new Vector3(enemyUnit.transform.position.x, 1, enemyUnit.transform.position.z), Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        dialogueText.text = $"A {enemyUnit.itemConsumes[0].itemName} fell from the fallen {enemyUnit.unitName}";
        yield return new WaitForSeconds(1.5f);
        dialogueText.text = $"You can use this to heal later on!";
        yield return new WaitForSeconds(1.5f);
        Destroy(enemyUnit.gameObject);

        // camera is mostly restored elsewhere. Unzoom here
        mainCamera.fieldOfView = 60;

        StartCoroutine(theBadger.BattleExit());
        //yield break;
    }
}