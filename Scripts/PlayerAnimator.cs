using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    private Animator _animator;
   

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void SetWalkAnimation(bool isWalk)
    {
        _animator.SetBool("isWalk",isWalk);
    }

    public void CallAnimationPlayerAttack()
    {
        _animator.SetTrigger("Attack");
    }
    
   
}
