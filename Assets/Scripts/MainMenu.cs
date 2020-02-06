using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Player");
    }

    public void QuitGame ()
    {
        Debug.Log("RIP RAR, died too soon");
        Application.Quit();
    }
}
