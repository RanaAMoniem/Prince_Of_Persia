using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Acrobatics : MonoBehaviour
{
    public LayerMask whatIsWall;
    public float wallrunForce, maxWallSpeed;
    bool isWallRight, isWallLeft;

    private Rigidbody rb;
    public Transform orientation;
    private Animator animator;

    private void WallRunInput() //make sure to call in void Update
    {
        //Wallrun
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.D) && isWallRight) StartWallrun();
        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A) && isWallLeft) StartWallrun();
    }
    private void StartWallrun()
    {   
        rb.useGravity = false;
        if(isWallRight)
            animator.SetBool("WallRunRight",true);
        if(isWallLeft)
            animator.SetBool("WallRunLeft",true);
    
        rb.AddForce(orientation.forward * wallrunForce * Time.deltaTime);
        rb.AddForce(Vector3.up*wallrunForce*Time.deltaTime*0.1f);


        //Make sure char sticks to wall
        if (isWallRight)
            rb.AddForce(orientation.right * wallrunForce / 5 * Time.deltaTime);
        else
            rb.AddForce(-orientation.right * wallrunForce / 5 * Time.deltaTime);
    }
    private void StopWallRun()
    {
        rb.useGravity = true;
        animator.SetBool("WallRunRight",false);
        animator.SetBool("WallRunLeft",false);


    }
    private void CheckForWall() //make sure to call in void Update
    {
        isWallRight = Physics.Raycast(transform.position, orientation.right, 1f, whatIsWall);
        isWallLeft = Physics.Raycast(transform.position, -orientation.right, 1f, whatIsWall);
        
        //leave wall run
        if (!isWallLeft && !isWallRight || !Input.GetKeyDown(KeyCode.A) && !Input.GetKeyDown(KeyCode.D)) {
            StopWallRun();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        CheckForWall();
        WallRunInput();
        
    }
}
