using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToBossLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
      SceneManager.LoadScene(3); //channgedd 
    }
}
