using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game state")]
    public float score;
    public float highScore;
    public ValueModifier enemyModifier;
    public ValueModifier playerModifier;
    // public ArenaModifier arenaModifier;

    [Header("Scene management")]
    public Hurtbox playerHurtBox;
    public bool isArenaPlaying = true;

    // Start is called before the first frame update
    void Start()
    {
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
        Debug.Log("Game is closed");
        Application.Quit();
    }


    public void GameOver()
    {
        if (isArenaPlaying) {
            isArenaPlaying = false;
            Debug.Log("Goes to Game Over screen");
            SceneManager.LoadScene("GameOver");
        }
    }

    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
