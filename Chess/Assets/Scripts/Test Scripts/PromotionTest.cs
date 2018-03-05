using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromotionTest : MonoBehaviour {

    public GameObject PromoteMenu;
    public GameObject PromoteButton;

    public void Promote()
    {
        PauseGame();
    }

    public void Button_clicked(string name)
    {
        Debug.Log(name + " button was clicked");
        ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        PromoteMenu.SetActive(true);
        PromoteButton.SetActive(false);
        Input.GetKeyDown(KeyCode.Escape);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        PromoteMenu.SetActive(false);
        PromoteButton.SetActive(true);
    }
}
