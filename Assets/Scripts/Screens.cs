using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screens : MonoBehaviour
{
    public  bool isPaused ;
     // to be removed -- player's script elmafro yehotha w ye set it be true lama yeb2a gameover 
    public GameObject PauseScreen;
    public GameObject GameOverScreen; //
    public GameObject Background;
    public Scene currentScene;
    public AudioSource menuTrack;
     public AudioSource bossTrack;
    bool gameOver;
     public Player_control playerr; // to be added */


    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        isPaused = false;
        playerr.isGameOver = false;
        
        
    }
    void Update()

    {
         gameOver = playerr.isGameOver; // to be added */
        if (!isPaused && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) )
        {
            Pause();
            
        }

         //to be removed and replaced with the commented one below 


        if (gameOver)
        {
            Debug.Log("gowa screen");
            playerr.playdie = false;
            GameOver();
            
            
        } 

    }



    public void playGame()
    {
        SceneManager.LoadScene(1); // 1.. 2nd one in queue
        Time.timeScale = 1f;
        bossTrack.Play();
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
        menuTrack.Stop();
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
        playerr.isGameOver = false;
        
        

        
    }
    public void QuitToMainMenu()
    {
        isPaused = false;
        playerr.isGameOver = false;
        GameOverScreen.SetActive(false);
        
    }


}
        
    

