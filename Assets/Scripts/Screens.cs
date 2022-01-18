using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Screens : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isGameOver = false; // players script elmafro yehotha w ye set it be true lama yeb2a gameover 
    public GameObject PauseScreen;
    public GameObject GameOverScreen; //
    public GameObject Background;
    public Scene currentScene;
    public AudioSource menuTrack;

    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }
    void Update()
    {
        if (!isPaused && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) )
        {
            Pause();
            
        }

        if (isGameOver  )
        {
            GameOver();
        }
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
        SceneManager.LoadScene(currentScene.name); //level 1 
        isPaused = false;
    }

    public void Pause()
    {
        PauseScreen.SetActive(true);
        Background.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        menuTrack.Play();
    }

    public void Resume()
    {
        PauseScreen.SetActive(false);
        Background.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        menuTrack.Stop();
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
    }


}
        
    

