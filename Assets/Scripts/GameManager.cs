using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum GameStatus {
    menu, start, running, end
}

public enum ArenaType {
    Sand,
    Snow,
    Mountains,
    Forest,
}

public struct ArenaModifierEntry {
    public ArenaModifier type;
    public GameObject obj;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game state")]
    public static int waveCounter;
    public static float score;
    public static float highScore;
    public static ValueModifier enemyModifier;
    public static ValueModifier playerModifier;
    public static ArenaModifier arenaModifier;

    [Header("Scene management")]
    public GameStatus status;
    public GameObject player;
    public ArenaModifierEntry[] arenaModifiers;
    private Dictionary<ArenaModifier, GameObject> _arenaModifiers;

    [Header("Transition settings")]
    public float startWaitTime = 3f;
    public float endWaitTime = 3f;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        player = GameObject.Find("Player");
        if(player != null) {
            status = GameStatus.start;
            counter = startWaitTime;
        } else {
            status = GameStatus.menu;
        }

        // spawn arena modifiers
        _arenaModifiers = new Dictionary<ArenaModifier, GameObject>();
        foreach(ArenaModifierEntry e in arenaModifiers) {
            _arenaModifiers.Add(e.type, e.obj);
        }

        Instantiate(_arenaModifiers[arenaModifier], Vector3.zero, Quaternion.identity, this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(isArenaPlaying && player != null && player.GetComponent<Hurtbox>().isAlive == false ) {
            Debug.Log("aww he ded");
            GameOver();
        }

        switch(status) {
            case GameStatus.start:
                counter -= Time.deltaTime;
                if(counter <= 0f) {
                    status = GameStatus.running;
                }
                break;

            case GameStatus.end:
                counter -= Time.deltaTime;
                if(counter <= 0f) {
                    ToGameOverScene();
                }
                break;

            default:
            case GameStatus.running:
                break;
        }
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
            status = GameStatus.end;
            counter = endWaitTime;
        }
    }


    public void ToArenaScene()
    {
        ToArenaScene(ArenaType.Sand);
    }

    public void ToArenaScene(ArenaType arena)
    {
        SceneManager.LoadScene("arena_" + arena.ToString());
    }

    public void ToGameOverScene() {
        instance = null;
        Debug.Log("Goes to Game Over screen");
        SceneManager.LoadScene("GameOver");
    }

    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public bool isArenaPlaying { get { return this.status == GameStatus.running; } }
}
