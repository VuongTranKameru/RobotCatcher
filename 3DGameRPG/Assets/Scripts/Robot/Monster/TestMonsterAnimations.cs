using UnityEngine;

public class TestMonsterAnimations : MonoBehaviour
{
    public MonsterAnimator monsterAnimator;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            monsterAnimator.PlayNormalAttack();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            monsterAnimator.PlaySkill();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            monsterAnimator.PlayIsDamaged();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            monsterAnimator.PlayIdle();
        }
    }
}