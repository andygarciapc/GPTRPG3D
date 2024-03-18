using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;
    private bool isReadyToAttack = false;
    private bool hasAnimator;
    private int animIDatk;

    private void Start()
    {
        hasAnimator = TryGetComponent(out animator);
        AssignAnimationIDs();
    }

    void Update()
    {
    }

    private void AssignAnimationIDs()
    {
        animIDatk = Animator.StringToHash("AttackTrigger");
    }

    public void LaunchAttack()
    { 
        // update animator if using character
        if (hasAnimator)
        {
            animator.SetTrigger(animIDatk);
        }
    }
}