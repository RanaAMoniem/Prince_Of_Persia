using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.AI;
using UnityEngine.UI;



public class AI : MonoBehaviour
{
    public enum WanderType { Random, Waypoint};
    public WanderType wanderType = WanderType.Waypoint;
    public FirstPersonController fpsc;
    public float wanderSpeed = 1.25f;
    public float chaseSpeed = 3f;
    private bool isAware = false;
    public float fov = 200f;
    public float viewDistance = 10f;
    private UnityEngine.AI.NavMeshAgent agent;
    public float wanderRadius = 7f;
    private Vector3 wanderPoint;
    public Transform[] waypoints;
    private int waypointIndex = 0;
    private Animator animator;
    public int health = 30;
    private Collider[] ragdollColliders;
    private Rigidbody[] ragdollRigidBodies;
    public AudioSource zombieGettingHitSound;
    public AudioSource zombieDyingSound;
    public AudioSource zombieVoiceOver;
    public AudioSource zombieWalking;
    public Slider slider;
    public GameObject healthBarUI;
    public GameObject player;
    // private string walkAudioBeingPlayedBy = "";

    public AudioSource ASExploring;
    bool exploreIsPlaying = false;
    public AudioSource ASFighting;
    bool fightIsPlaying = false;
    bool inFight = false;

    public Player_control playerr;
    bool stop;
    public GameObject sOT;
    public bool zombieDied = false;


    public void SearchForPlayer(){


        if(Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < fov / 2f){

            if(Vector3.Distance(player.transform.position, transform.position) < viewDistance){
                RaycastHit hit;
                if(Physics.Linecast(transform.position, player.transform.position, out hit, -1)){

                    if(hit.transform.CompareTag("Player")){

                        OnAware(false);
                    }


                }

            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // ASExploring = gameObject.AddComponent<AudioSource>();
        // ASFighting = gameObject.AddComponent<AudioSource>();


        zombieWalking.Stop();
        zombieGettingHitSound.Stop();
        zombieDyingSound.Stop();
        zombieVoiceOver.Stop();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        gameObject.tag = "Zombie";
        wanderPoint = RandomWanderPoint();
        animator = GetComponentInChildren<Animator>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidBodies = GetComponentsInChildren<Rigidbody>();

        foreach (Collider col in ragdollColliders){
            if(!col.CompareTag("Zombie")){
                col.enabled = false;
            }
        }

        foreach (Rigidbody rb in ragdollRigidBodies){

            rb.isKinematic = true;
        }

        slider.value = health;

    }



    // Update is called once per frame
    void Update()
    {
        
        stop = playerr.activated;

        if (stop)
        {
            StartAbility();
        }
        else
        {


            /*if (Input.GetKeyDown(KeyCode.D))
            {
                Die();
                return;
            }*/



            if (health <= 0)
              {
                  Die();
                  return;
              }
            
 

              if (Vector3.Distance(transform.position, waypoints[0].position) < 20f)
                {

                    if (isAware)
                    {

                        // if(Vector3.Distance(transform.position, waypoints[0].position) > 10f){

                        //     // Wander();
                        //     isAware = false;
                        //     waypointIndex = 0;
                        //     // animator.SetBool("Aware", false);
                        //     // agent.speed = wanderSpeed;


                        // }


                        agent.SetDestination(player.transform.position);
                        animator.SetBool("Aware", true);
                        agent.speed = chaseSpeed;


                    }
                    else
                    {

                        Wander();
                        SearchForPlayer();
                        animator.SetBool("Aware", false);
                        agent.speed = wanderSpeed;

                    }
                }

                else
                {

                    Wander();
                    animator.SetBool("Aware", false);
                    agent.speed = wanderSpeed;
                    isAware = false;
                    if (!isAware)
                    {
                        inFight = false;
                    }
                }


                slider.value = health;
            
            
        }
    }

    public void OnAware(bool flag){
        isAware = true;
        inFight = true;
        if(!flag){
            zombieVoiceOver.Play();
            if(fightIsPlaying == false){
              ASFighting.Play();
              fightIsPlaying = true;
              if(exploreIsPlaying){
                ASExploring.Stop();
                exploreIsPlaying = false;
              }

            }
        }
    }

    public void ZombiePunch(){

        animator.Play("ZombiePunch");
    }

    public Vector3 RandomWanderPoint(){

        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);

    }

    public void Wander(){
        if(exploreIsPlaying == false && isAware == false && inFight == false){

          ASExploring.Play();
          exploreIsPlaying = true;
          if(fightIsPlaying){
            ASFighting.Stop();
            fightIsPlaying = false;
          }

        }

        if(wanderType == WanderType.Random){

            if(Vector3.Distance(transform.position, wanderPoint) < 2f){
                wanderPoint = RandomWanderPoint();
            }

            else{
                agent.SetDestination(wanderPoint);
            }

        }

        else{


            // if(walkAudioBeingPlayedBy == "" || walkAudioBeingPlayedBy == transform.parent.name){

            //     if(!zombieWalking.isPlaying && Vector3.Distance(player.transform.position, transform.position) < 20f){

            //         zombieWalking.Play();
            //         walkAudioBeingPlayedBy = transform.parent.name;

            //     }


            //     else if(zombieWalking.isPlaying && Vector3.Distance(player.transform.position, transform.position) > 20f){

            //         zombieWalking.Stop();
            //         walkAudioBeingPlayedBy = "";


            //     }


            // }
            if(Vector3.Distance(waypoints[waypointIndex].position, transform.position) < 2f){

                if(waypointIndex == waypoints.Length - 1){
                    waypointIndex = 0;

                }

                else{
                    waypointIndex++;
                }

            }

            else{


                agent.SetDestination(waypoints[waypointIndex].position);
            }
        }
    }

    public void OnHit(int damage){

        health -= damage;
        if(health > 0){
            zombieGettingHitSound.Play();
        }

        else{

            zombieDyingSound.Play();
            
        }
    }

    public void Die(){
        zombieDied = true;
        agent.speed = 0;
        animator.enabled = false;
        slider.value = 0;
        healthBarUI.SetActive(false);
        foreach(Collider col in ragdollColliders){
            col.enabled = true;
        }

        foreach(Rigidbody rb in ragdollRigidBodies){
            rb.isKinematic = false;
        }

        Instantiate(sOT, agent.transform.position, Quaternion.identity);

    }

    IEnumerator SandsOfTime()
    {
        animator.enabled = false;
        agent.speed = 0;
        yield return new WaitForSeconds(5);
        animator.enabled = true;
        agent.speed = 1.5f;
        
    }

    void StartAbility()
    {
        
        StartCoroutine(SandsOfTime());
    }


}