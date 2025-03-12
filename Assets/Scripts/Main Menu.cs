using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    private string levelToLoad;

    public void PlayGame()
    {
        SceneManager.LoadScene(levelToLoad);
        Debug.Log("Loaded scene works!!!!!");
    }

    public void QuitGame()
    {
    Application.Quit();
    }

    void Start()
    {

    }


    void Update()
    {

    }
}
