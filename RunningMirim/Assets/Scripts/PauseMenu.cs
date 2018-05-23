using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {
    
    //public string mainMenuLevel;
    public GameObject pauseMenu;
    public GameController GC;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        GC.isGamePlaying = false;
        Character.isTriggerOn = false;//충돌처리 안되도록
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        GC.isGamePlaying = true;
        Character.isTriggerOn = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        GC.isGamePlaying = true;
        //리셋
        //GC.jellyScore = 0;
        Character.isTriggerOn = true;
        SceneManager.LoadScene("InGame");
    }

    public void QuitToMain()
    {
        Time.timeScale = 1f;
        Application.Quit();
        Character.isTriggerOn = true;
        SceneManager.LoadScene("RankMenu");
    }
}
