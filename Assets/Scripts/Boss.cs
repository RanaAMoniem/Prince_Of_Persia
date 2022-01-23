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
        bossTrack.Play();

    }
    private void FixedUpdate()

    {
        stop = playerr.activated; //toka added 
        paused = screen.isPaused;                  // toka added 
        m_Animator.SetBool("dead", false);
        m_Animator.SetBool("walk", false);
        m_Animator.SetBool("fierce", false);
        m_Animator.SetBool("attack", false);

        if (stop)
        {
            Debug.Log("activated");

            m_Animator.enabled = false;


        }
        else
        {
            m_Animator.enabled = true; //
            Vector3 target = new Vector3((player.transform.position.x + 0.1f), m_Rigidbody.position.y, player.transform.position.z + 0.1f);
            Vector3 newPos = Vector3.MoveTowards(m_Rigidbody.position, target, speed * Time.fixedDeltaTime);
            float distance = Vector3.Distance(player.transform.position, m_Rigidbody.position);
            transform.LookAt(player.transform);
           // Debug.Log(distance);
            if (isDead)
            {
                m_Animator.SetBool("dead", true);
                return;
            }
            if (distance <= 3.4)
            {
                m_Animator.SetBool("walk", false);
                if (isFierce)
                {
                    //Debug.Log("FIERCE");
                    m_Animator.SetBool("fierce", true);
                }

                else
                {
                    //Debug.Log("ATTACK");
                    m_Animator.SetBool("attack", true);

                }
            }

            else
            {
                //Debug.Log("WALK");
                m_Animator.SetBool("walk", true);
                m_Rigidbody.MovePosition(newPos);
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
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
        Debug.Log("BOSS HEALTH");
        Debug.Log(currentHealth);
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
        playerr.TakeDamage(10);
    }

    void Die()
    {
        isDead = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.tag);
        if (collision.gameObject.tag == "Player")
        {
            if (m_Animator.GetBool("attack") || m_Animator.GetBool("fierce"))
            {
                AtackPlayer();
                //Debug.Log(Vector3.Distance(player.transform.position, m_Rigidbody.position));
                Debug.Log("BOSS ATTACKING PLAYER");
            }

        }
    }

}
