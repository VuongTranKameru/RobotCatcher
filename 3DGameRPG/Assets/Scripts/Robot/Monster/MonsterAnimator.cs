using UnityEngine;

public class MonsterAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Get the Animator component attached to the monster
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    // Play the normal attack animation
    public void PlayNormalAttack()
    {
        animator.SetTrigger("NormalAttack");
    }

    // Play the skill animation
    public void PlaySkill()
    {
        animator.SetTrigger("Skill");
    }

    // Play the is damaged animation
    public void PlayIsDamaged()
    {
        animator.SetTrigger("IsDamaged");
    }

    // Play the idle animation
    public void PlayIdle()
    {
        animator.SetTrigger("Idle");
    }
}