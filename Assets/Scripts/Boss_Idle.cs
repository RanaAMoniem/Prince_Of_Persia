using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Idle : StateMachineBehaviour
{
    Rigidbody m_Rigidbody;
    GameObject player;
    public float speed = 1f;
    Boss boss;
    float distance;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Rigidbody = animator.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        boss = animator.GetComponent<Boss>();
        animator.SetBool("idle", true);

    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
        //Debug.Log(Vector3.Distance(player.transform.position, m_Rigidbody.position));
        distance = Vector3.Distance(player.transform.position, m_Rigidbody.position);

        if (distance <= 1.9)
        {
            animator.SetBool("attack", true);
            
            
        }

        else
        {
            animator.SetBool("walk", true);
        }


    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        animator.SetBool("idle", false);
        
    }

}

