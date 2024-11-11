using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRPG : MonoBehaviour
{
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void RegAttackAnim()
    {
        anim.SetTrigger("checkAttack");
        anim.SetFloat("Attacked", 0f);
    }

    public void SpecialAttackAnim()
    {
        anim.SetTrigger("checkAttack");
        anim.SetFloat("Attacked", 1f);
    }

    public void CastAnim()
    {
        anim.SetTrigger("checkAttack");
        anim.SetFloat("Attacked", 2f);
    }

    public void IsHitAnim()
    {
        anim.SetTrigger("checkGetHit");
    }
}
