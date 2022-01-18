using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExample : MonoBehaviour
{
    public Transform spherecastSpawn;
    public LayerMask zombieLayer; 
    public int attackDamage = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){

            AttackZombie();
        }
    }

    private void OnTriggerEnter(Collider other) {
        
        if(other.gameObject.name == "Zombie"){
            
            other.GetComponent<AI>().OnAware();
        }
    }

    public void AttackZombie(){

        
        RaycastHit hit;
        if(Physics.SphereCast(spherecastSpawn.position, 0.5f, spherecastSpawn.TransformDirection(Vector3.forward), out hit, zombieLayer)){
            if(hit.transform.GetComponent<AI>() != null){ 
                hit.transform.GetComponent<AI>().OnHit(attackDamage);
            }
        }
    }
}
