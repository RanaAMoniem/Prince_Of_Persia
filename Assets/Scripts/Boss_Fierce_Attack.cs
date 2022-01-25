using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Fierce_Attack : StateMachineBehaviour
{
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.SetBool("fierce", true);
        animator.SetBool("walk", false);
        animator.SetBool("attack", false);
    }
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      

    }
}
