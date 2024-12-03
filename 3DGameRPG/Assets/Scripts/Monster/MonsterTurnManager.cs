using UnityEngine;

public class MonsterTurnManager : MonoBehaviour
{
    private MonsterAnimator monsterAnimator;

    void Start()
    {
        // Get the MonsterAnimator component
        monsterAnimator = GetComponent<MonsterAnimator>();
    }

    public void MonsterTurn()
    {
        // Decide what the monster does on its turn
        int action = Random.Range(0, 3); // Random number between 0 and 2

        if (action == 0)
        {
            Debug.Log("Monster uses Normal Attack!");
            monsterAnimator.PlayNormalAttack();
        }
        else if (action == 1)
        {
            Debug.Log("Monster uses Skill!");
            monsterAnimator.PlaySkill();
        }
        else if (action == 2)
        {
            Debug.Log("Monster gets damaged!");
            monsterAnimator.PlayIsDamaged();
        }

        // After a delay, return to idle
        Invoke("ReturnToIdle", 2.0f);
    }

    private void ReturnToIdle()
    {
        monsterAnimator.PlayIdle();
        Debug.Log("Monster returns to idle.");
    }
}