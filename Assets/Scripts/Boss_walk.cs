using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_walk : StateMachineBehaviour
{
    Rigidbody m_Rigidbody;
    GameObject player;
    public float speed = 6f;
    Boss boss;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_Rigidbody = animator.GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
        
       
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 target = new Vector3((player.transform.position.x + 0.1f), m_Rigidbody.position.y, player.transform.position.z + 0.1f);
        Vector3 newPos = Vector3.MoveTowards(m_Rigidbody.position, target, speed * Time.fixedDeltaTime);
        float distance = Vector3.Distance(player.transform.position, m_Rigidbody.position);
        animator.transform.LookAt(player.transform);
        //Debug.Log(distance);
        if (distance <= 1.9)
        {
            animator.SetBool("attack", true);
            
        }

        else
        {
            animator.SetBool("walk", true);
            m_Rigidbody.MovePosition(newPos);
            
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("walk", false);

    }
}
