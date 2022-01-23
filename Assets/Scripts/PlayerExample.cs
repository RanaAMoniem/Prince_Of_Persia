using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExample : MonoBehaviour
{
    public Transform spherecastSpawn;
    public LayerMask zombieLayer; 
    public int attackDamage = 10;
    public AudioSource zombieWalking;
    public GameObject zombie;
    public GameObject zombie1;
    public GameObject zombie2;
    public GameObject zombie3;
    public GameObject zombie4;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!zombieWalking.isPlaying && (Vector3.Distance(transform.position, zombie.transform.position) < 20f || Vector3.Distance(transform.position, zombie1.transform.position) < 20f || Vector3.Distance(transform.position, zombie2.transform.position) < 20f || Vector3.Distance(transform.position, zombie3.transform.position) < 20f ||  Vector3.Distance(transform.position, zombie4.transform.position) < 20f)){
            zombieWalking.Play();
        }

        else if(zombieWalking.isPlaying && (Vector3.Distance(transform.position, zombie.transform.position) > 20f && Vector3.Distance(transform.position, zombie1.transform.position) > 20f && Vector3.Distance(transform.position, zombie2.transform.position) > 20f && Vector3.Distance(transform.position, zombie3.transform.position) > 20f && Vector3.Distance(transform.position, zombie4.transform.position) > 20f)){

            zombieWalking.Stop();

        }
    }

    private void OnTriggerEnter(Collider other) {
        
        
        if(other.gameObject.name == "Zombie"){
            
            other.GetComponent<AI>().OnAware(true);
            other.GetComponent<AI>().ZombiePunch();
        }
    }

    public void AttackZombie(){

        //Call the OnHit method in AI script
            
    }
}

