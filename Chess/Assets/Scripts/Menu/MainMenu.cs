using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayVsPlayer()
    {
        SceneManager.LoadScene("ChessBoard");
    }

    public void PlayVsAI()
    {
        SceneManager.LoadScene("PlayVSAI");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!!!");
        Application.Quit();
    }
}
