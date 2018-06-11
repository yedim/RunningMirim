using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour {

    public string playGameLevel;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(playGameLevel);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(playGameLevel);
        }
    }
}
