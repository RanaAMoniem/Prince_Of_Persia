using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princess : MonoBehaviour
{
    // Start is called before the first frame update
    Animator p_animator;
    public ThirdPersonController playerr;
    bool stop;

    void Start()
    {
        p_animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        stop = playerr.activated;
        if (stop)
        {
            Debug.Log("cry");
            p_animator.enabled = false;
        }
        else
        {
            p_animator.enabled = true;
        }
    }
}
