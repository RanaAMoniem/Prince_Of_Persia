using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screens : MonoBehaviour
{
    public  bool isPaused ;
    public static bool isGameOver = false; // to be removed -- player's script elmafro yehotha w ye set it be true lama yeb2a gameover 
    public GameObject PauseScreen;
    public GameObject GameOverScreen; //
    public GameObject Background;
    public Scene currentScene;
    public AudioSource menuTrack;
     public AudioSource bossTrack;
    bool gameOver;
    /* public ThirdPersonController playerr; // to be added */

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        isPaused = false;
    }
    void Update()

    {
       /*  gameOver = playerr.isGameOver; // to be added */
        if (!isPaused && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) )
        {
            Pause();
            
        }

        if (isGameOver  )
        {
            GameOver();
        } //to be removed and replaced with the commented one below 


        /*if (gameOver)
        {
            GameOver();
        } */
    }



    public void playGame()
    {
        SceneManager.LoadScene(1); // 1.. 2nd one in queue
        Time.timeScale = 1f;
 }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void RestartLevel()
    {
        Debug.Log(currentScene.name);
        SceneManager.LoadScene(currentScene.name); 
        isPaused = false;
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        PauseScreen.SetActive(true);
        Background.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        menuTrack.Play();
        bossTrack.Stop();
    }

    public void Resume()
    {
        PauseScreen.SetActive(false);
        Background.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        menuTrack.Stop();
        bossTrack.Play();
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
        Background.SetActive(true);
        Time.timeScale = 0f;
        
    }
    public void QuitToMainMenu()
    {
        isPaused = false;
        menuTrack.Play();
    }


}
        
    

