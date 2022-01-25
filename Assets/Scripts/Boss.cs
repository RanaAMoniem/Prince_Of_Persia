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
    public bool isFierce;
    public bool isDead;
    public bool isAttack;
    public float speed = 1f;
    public bool isCollided;


    public AudioSource bossTrack;
    public Player_control playerr; //toka added -- esm el script beta3 player 3and omar(ThirdPersonController)
    bool stop; // toka added 
    public Screens screen;
    bool paused;

    


    //
    //

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
        isAttack = false;
        isCollided = false;
        bossTrack.Play();

    }
    private void FixedUpdate()

    {
        
        stop = playerr.activated; //toka added 
        paused = screen.isPaused;                  // toka added 
        if (stop)
        {
            Debug.Log("activated");
            m_Animator.enabled = false;
        }
        else
        {
            m_Animator.enabled = true; 
            if (isDead)
            {
                m_Animator.SetBool("dead", true);
                return;
            }
            if (isFierce)
            {
                m_Animator.SetBool("fierce", true);
                m_Animator.SetBool("attack", false);
                return;
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isAttack && isCollided);
        paused = screen.isPaused;

        if (paused)
        {
            Debug.Log("firstif");
            bossTrack.Stop();
        }
    }


    public void TakeDamage(int damage)
    {

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth == 0)
        {
          
            Die();
            return;
        }
        if (currentHealth <= 80)
        {
            isFierce = true;

        }
    }

    public void AtackPlayer()
    {
        if (isAttack && isCollided || isFierce && isCollided)
        {
            Debug.Log("BOSS ATTACKS PLAYER");
            playerr.TakeDamage(10);
        }
        isCollided = false;
        isAttack = false;
       
    }

    void Die()
    {
        isDead = true;
        
    }

    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.tag);
        if (collision.gameObject.tag == "Player")
        {
            isCollided = true;

        }
    }
    public bool isIdle()
    {
        return m_Animator.GetBool("idle");
    }

   

}
