using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromotionTest : MonoBehaviour {

    public GameObject PromoteMenu;
    public GameObject PromoteButton;
    public void promote()
    {
        pauseGame();
    }

    public void button_clicked(string name)
    {
        Debug.Log(name + " button was clicked");
        resumeGame();
    }

    public void pauseGame()
    {
        Time.timeScale = 0f;
        PromoteMenu.SetActive(true);
        PromoteButton.SetActive(false);
        Input.GetKeyDown(KeyCode.Escape);
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        PromoteMenu.SetActive(false);
        PromoteButton.SetActive(true);
    }
}
