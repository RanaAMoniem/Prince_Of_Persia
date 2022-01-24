using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_attack : StateMachineBehaviour
{
    Rigidbody m_Rigidbody;
    GameObject player;
    public float speed = 1f;
    Boss boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Rigidbody = animator.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator.SetBool("idle", false);
        boss = animator.GetComponent<Boss>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.isAttack = true;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        animator.SetBool("attack", false);

    }
}
