using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("arena_Sand");
    }

    public void QuitGame ()
    {
        Debug.Log("Game is closed");
        Application.Quit();
    }

    bool gameHasEnded = false;

    public void GameOver ()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Goes to Game Over screen");
            Invoke("toGameOverScreen", 2f);
        }
    }

    public void toMainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    void toGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
