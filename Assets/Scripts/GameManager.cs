using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Hurtbox playerHurtBox;
    public bool isArenaPlaying = true;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        if(isArenaPlaying) {
            playerHurtBox = GameObject.Find("Player").GetComponent<Hurtbox>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isArenaPlaying && playerHurtBox != null && playerHurtBox.isAlive == false ) {
            Debug.Log("aww he ded");
            GameOver();
        }
    }

    public void StartGame()
    {
        isArenaPlaying = true;
        SceneManager.LoadScene("arena_Sand");
    }

    public void QuitGame()
    {
        instance = null;
        Debug.Log("Game is closed");
        Application.Quit();
    }


    public void GameOver()
    {
        if (isArenaPlaying) {
            isArenaPlaying = false;
            instance = null;
            Debug.Log("Goes to Game Over screen");
            SceneManager.LoadScene("GameOver");
        }
    }

    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
