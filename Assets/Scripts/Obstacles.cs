using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    // Start is called before the first frame update
   
    public ThirdPersonController playerr;
    bool stop;


    Animation obs_an;

    void Start()
    {
       
       
        
        obs_an = GetComponent<Animation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stop = playerr.activated;
        if (stop)
        {
            Debug.Log("gowa el if");
            obs_an.enabled = false;
            
        }
        else
        {
            obs_an.enabled = true;
        }
    }
}
