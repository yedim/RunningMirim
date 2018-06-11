using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour {
    
    public GameObject pauseMenu;

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        GameController.isGamePlaying = false;
        Character.isTriggerOn = false;//충돌처리 안되도록
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        GameController.isGamePlaying = true;
        Character.isTriggerOn = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        GameController.isGamePlaying = true;
        //리셋
        Character.isTriggerOn = true;
        SceneManager.LoadScene("InGame");
    }

    public void QuitToMain()
    {
        Time.timeScale = 1f;
        Character.isTriggerOn = true;
        SceneManager.LoadScene("RankMenu");
    }
}
