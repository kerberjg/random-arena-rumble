using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Hurtbox playerHurtBox;
    public bool isPlaying = true;

    static bool gameHasEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        try {
        playerHurtBox = GameObject.Find("Player").GetComponent<Hurtbox>();
        } catch(Exception e) {
            Debug.Log("No Player in Scene");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying && playerHurtBox != null && playerHurtBox.isAlive == false ) {
            Debug.Log("aww he ded");
            GameOver();
        }
    }

    public void StartGame()
    {
        gameHasEnded = false;
        SceneManager.LoadScene("arena_Sand");
    }

    public void QuitGame()
    {
        Debug.Log("Game is closed");
        Application.Quit();
    }


    public void GameOver()
    {
        if (gameHasEnded == false) {
            gameHasEnded = true;
            Debug.Log("Goes to Game Over screen");
            SceneManager.LoadScene("GameOver");
        }
    }

    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
