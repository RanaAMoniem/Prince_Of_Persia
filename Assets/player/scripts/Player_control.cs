using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_control : MonoBehaviour
{
    private Animator animator;
    public AudioSource FootSteps;
    public AudioSource die;
    public AudioSource hit;
    public AudioSource soundOfTime;
    public int maxHealth = 100;
    public int currentHealth_player;
    public HealthBar_player healthBar;
    private bool playdie;
    public Boss boss;
    public GameObject zombie;
    private bool attackTrue;
    public bool activated;
    private int sandOfTime;

    public bool isGameOver;



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth_player = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playdie = false;
        attackTrue = false;
        activated = false;
        sandOfTime = 0;
       isGameOver = false;
    }

    public void TakeDamage(int damage)
    {
        currentHealth_player -= damage;
        healthBar.SetHealth(currentHealth_player);
        if (currentHealth_player > 0)
        {
            hit.Play();
        }
        if (playdie == false)
        {
            if (currentHealth_player <= 0)
            {
                die.Play();
            }
        }


    }
    IEnumerator SandsOfTime()
    {
        Debug.Log("in enum");
        activated = true;
        Debug.Log(activated);
        yield return new WaitForSeconds(5);
        activated = false;
        Debug.Log(activated);
    }

    void StartAbility()
    {
        Debug.Log("in method");
        StartCoroutine(SandsOfTime());
    }

    private void died()
    {
        if (currentHealth_player <= 0)
        {
            animator.SetTrigger("died");
            playdie = true;
            isGameOver = true;
			}
		}
        
    void attackbegins()
    {
        attackTrue = true;
        
    }

    void attackends()
    {
        attackTrue = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(attackTrue);
        if (collision.gameObject.tag == "boss")
        {
            Debug.Log("PLAYER COLLIDES");
            if (boss.isIdle())
            {
               Debug.Log("BOSS IDLE");
                if (attackTrue)
                {
                    Debug.Log("PLAYER ATTACKS BOSS");
                    boss.TakeDamage(40);
                }
            }
        }

        if (collision.gameObject.tag == "Zombie")
        {
            if (attackTrue)
            {
                //    zombie.TakeDamage(10);
            }
        }
       
        if(collision.gameObject.tag == "sandsOfTime"){
                Destroy(collision.gameObject);
                sandOfTime+=1;
            Debug.Log("entered");
        }
        if (collision.gameObject.tag == "obstacle")
        {
            // died();
            // gameover
        }
    }


    // Update is called once per frame
    void Update()
    {
        died();

        if (Input.GetKeyDown(KeyCode.W) | Input.GetKeyDown(KeyCode.D) | Input.GetKeyDown(KeyCode.S) | Input.GetKeyDown(KeyCode.A)
          | Input.GetKeyDown(KeyCode.LeftArrow) | Input.GetKeyDown(KeyCode.RightArrow) | Input.GetKeyDown(KeyCode.UpArrow) | Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!FootSteps.isPlaying)
            {
                FootSteps.Play();
            }
        }
        if (Input.GetKeyUp(KeyCode.W) | Input.GetKeyUp(KeyCode.D) | Input.GetKeyUp(KeyCode.S) | Input.GetKeyUp(KeyCode.A)
         | Input.GetKeyUp(KeyCode.LeftArrow) | Input.GetKeyUp(KeyCode.RightArrow) | Input.GetKeyUp(KeyCode.UpArrow) | Input.GetKeyUp(KeyCode.DownArrow))
        {
            FootSteps.Stop();
        }

        if (Input.GetKeyDown(KeyCode.R))
            animator.SetTrigger("rolls");

        if (Input.GetMouseButtonDown(0))
            animator.SetTrigger("attacks");


        if (Input.GetMouseButtonDown(1))
            animator.SetTrigger("defends");

        //   if (Input.GetKeyDown(KeyCode.K)){
        //     TakeDamage(10);
        // }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (sandOfTime > 0)
            {
                StartAbility();
                soundOfTime.Play();
                sandOfTime -= 1;
            }
        }



    }
}