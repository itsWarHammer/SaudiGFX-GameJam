using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class TurnBasedSystem : MonoBehaviour
{
    [SerializeField] bool playerTurn;
    [SerializeField] bool canPress;
    [SerializeField] bool rightAbility;
    [SerializeField] bool upAbility;
    [SerializeField] bool downAbility;

    [SerializeField] Image playersHealthBar;

    enum EnemyEmotion { Happy, Angry, Sad, Content }
    EnemyEmotion enemyEmotion;

    [SerializeField] string[] leftDialogue;
    [SerializeField] string[] rightDialogue;
    [SerializeField] string[] upDialogue;
    [SerializeField] string[] downDialogue;

    [SerializeField] TMP_Text dialogueText;

    [SerializeField] EnemyDialogue[] enemyDialogue;
    int enemyNumber;
    int currentDialogueNumber;

    [SerializeField] float textSpeed;


    private void Update()
    {
        if (!canPress) return;

        if (playerTurn)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                //dialogueText.text = leftDialogue[Random.Range(0,leftDialogue.Length)];
                Talk(0);
                StartCoroutine(TypeWritterEffect(leftDialogue[Random.Range(0, leftDialogue.Length)]));
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && rightAbility)
            {
                //dialogueText.text = rightDialogue[Random.Range(0, rightDialogue.Length)];
                Talk(1);
                StartCoroutine(TypeWritterEffect(rightDialogue[Random.Range(0, rightDialogue.Length)]));
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && upAbility)
            {
                //dialogueText.text = upDialogue[Random.Range(0, upDialogue.Length)];
                Talk(2);
                StartCoroutine(TypeWritterEffect(upDialogue[Random.Range(0, upDialogue.Length)]));
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && downAbility)
            {
                //dialogueText.text = downDialogue[Random.Range(0, downDialogue.Length)];
                Talk(3);
                StartCoroutine(TypeWritterEffect(downDialogue[Random.Range(0, downDialogue.Length)]));
            }
        }
    }

    private void Talk(int emotionNumber)
    {
        dialogueText.text = "";
        if(emotionNumber == (int)enemyEmotion)
        {
            print("Gain Chill!");
            currentDialogueNumber++;
        }
        else
        {
            print("Become Angry");
        }

        //playerTurn = false;
    }

    private IEnumerator TypeWritterEffect(string dialogue)
    {
        canPress = false;
        dialogueText.text = "";
        foreach(char character in dialogue)
        {
            dialogueText.text += character;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(2);

        if (playerTurn)
        {
            playerTurn = false;
            StartCoroutine(TypeWritterEffect(enemyDialogue[enemyNumber].dialogues[currentDialogueNumber]));
            print("Enemy turn");
        }
        else
        {
            print("Can press now");
            canPress = true;
            playerTurn = true;
        }
    }
}
