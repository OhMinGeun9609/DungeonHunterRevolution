using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void OnRunAnime(bool isRun)
    {
        animator.SetBool("isMove", isRun);
    }
    public void OnAttackAnime()
    {
        animator.SetTrigger("isAttack");
    }
    public void OnHitAnime()
    {
        animator.SetTrigger("isHit");
    }
    public void OnDeathAnime()
    {
        animator.SetTrigger("isDeath");
    }
    public void SetAnimeSpeed(float speed)
    {
        animator.speed = speed;
    }
}
