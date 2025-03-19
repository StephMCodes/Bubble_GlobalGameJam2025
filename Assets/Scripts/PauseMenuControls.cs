using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuControls : MonoBehaviour
{
    public string levelToLoad;
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturntoMainMenu()
    {
        SceneManager.LoadScene(levelToLoad);    
    }

}
