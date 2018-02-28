using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    /*public void PlayVsAIHard()
    {
        SceneManager.LoadScene(2);
        DiffcultyManager.Level = 6;
    }*/
   
    public void QuitGame()
    {
        Debug.Log("QUIT!!!");
        Application.Quit();
    }
}
