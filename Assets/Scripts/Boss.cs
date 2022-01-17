using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    GameObject player;
    public int maxHealth = 200;
    public int currentHealth;
    public HealthBar healthBar;
    Rigidbody m_Rigidbody;
    Animator m_Animator;
    BoxCollider m_box;
    bool isFierce;
    bool isDead;
    public float speed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_box = GetComponent<BoxCollider>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        isFierce = false;
        isDead = false;
        

}
    private void FixedUpdate()
    {
        Vector3 target = new Vector3((player.transform.position.x + 0.1f), m_Rigidbody.position.y, player.transform.position.z + 0.1f);

        Vector3 newPos = Vector3.MoveTowards(m_Rigidbody.position, target, speed * Time.fixedDeltaTime);
        
        float distance = Vector3.Distance(player.transform.position, m_Rigidbody.position);

        transform.LookAt(player.transform);
        Debug.Log(distance);
        if (isDead)
        {
            m_Animator.SetBool("dead", true);
            m_Animator.SetBool("attack", false);
            m_Animator.SetBool("fierce", false);
            m_Animator.SetBool("walk", false);
            return;
        }
        if (distance <= 1.6)
        {
            m_Animator.SetBool("walk", false);
            if (isFierce)
            {
                Debug.Log("FIERCE");
                m_Animator.SetBool("fierce", true);
                m_Animator.SetBool("attack", false);
            }

            else 
            {
                Debug.Log("ATTACK");
                m_Animator.SetBool("attack", true);
                m_Animator.SetBool("fierce", false);

            }
        }

        else {
            Debug.Log("WALK");
            m_Animator.SetBool("walk", true);
            m_Rigidbody.MovePosition(newPos);  
        }


       

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.CapsLock))
        {
            TakeDamage(40);
        }
        
    }


    public void TakeDamage(int damage)
    {
        if (currentHealth == 0)
        {
            Die();
            return;
        }

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        
        if (currentHealth <= 80)
        {
            isFierce = true;
            
        }
        

    }

    public void AtackPlayer()
    {
        //player.TakeDamage(10);
    }

    void Die()
    {
        isDead = true;
    }

    
}
