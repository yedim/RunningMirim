using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

    public void PlayGame(string playGameLevel)
    {
        //Scene scene = SceneManager.GetActiveScene();

        //int curScene = scene.buildIndex;

        //int playScene = curScene + 1;

        //SceneManager.LoadScene(playScene);
        SceneManager.LoadScene(playGameLevel);
    }
    public void ExplainGame(string playGameLevel)
    {
        SceneManager.LoadScene(playGameLevel);
    }
    public void BackMenu()
    {
        Scene scene = SceneManager.GetActiveScene();

        int curScene = scene.buildIndex;

        int backScene = curScene - 1;

        SceneManager.LoadScene(backScene);
    }
}
