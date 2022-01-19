using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public bool activated; // add
    

    void Start()
    {
        activated = false; // add
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Q)))
        {
            StartAbility(); //add
            Debug.Log("ability");
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


}

