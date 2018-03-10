using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

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

    public void HostGame()
    {
        UINetworkManager.Host = true;
        SceneManager.LoadScene("OnlinePVP");
    }

    public void JoinGame()
    {
        UINetworkManager.Client = true;
        SceneManager.LoadScene("OnlinePVP");
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!!!");
        Application.Quit();
    }

}
