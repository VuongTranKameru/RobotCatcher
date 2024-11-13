using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRPG : MonoBehaviour
{
    Animator anim;

    AnimationClip[] clips;
    float clipLength;

    public float ReadClipLength() { return clipLength; }

    void Awake()
    {
        anim = GetComponent<Animator>();
        clips = anim.runtimeAnimatorController.animationClips;
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

    void AnimationClipsTiming()
    {
        Debug.Log("anim: " + clips.Length); //can only read the clip name, not state name
        /*foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "Attacking":
                    attackTime = clip.length;
                    break;
                case "Damage":
                    damageTime = clip.length;
                    break;
                case "Dead":
                    deathTime = clip.length;
                    break;
                case "Idle":
                    idleTime = clip.length;
                    break;
            }
        }*/
    }
}
