using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] enum EnemyEmotion { Happy, Angry, Sad, Content }
    EnemyEmotion enemyEmotion;

    [SerializeField] string[] myDialogue;
    int currentTalkNumber;
}
