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

[System.Serializable]
public struct ArenaModifierEntry {
    public ValueModifier type;
    public GameObject obj;
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game state")]
    private static bool isInit = false;
    public static int waveCounter;
    public static float score;
    public static float highScore;
    public static ValueModifier enemyModifier;
    public static ValueModifier playerModifier;
    public static ValueModifier arenaModifier;

    [Header("Scene management")]
    public GameStatus status;
    private GameObject player;
    private GameObject enemyManager;
    public ArenaModifierEntry[] arenaModifiers;
    private Dictionary<ValueModifier, GameObject> _arenaModifiers;

    [Header("Transition settings")]
    public float startWaitTime = 3f;
    public float endWaitTime = 3f;
    private float counter;

    // Start is called before the first frame update
    void Start()
    {
        // setup game manager
        instance = this;

        if(!isInit) {
            ResetState();
            isInit = true;
        }

        // execute only in Arenas
        if(status == GameStatus.start) {
            // setup player
            player = GameObject.Find("Player");
            print(playerModifier.ToString());
            player.GetComponent<ModifierContainer>().modifier.MergeModifier(playerModifier);

            if(player != null) {
                status = GameStatus.start;
                counter = startWaitTime;
            } else {
                status = GameStatus.menu;
            }

            // setup enemies
            enemyManager = GameObject.Find("EnemyManager");
            enemyManager.GetComponent<ModifierContainer>().modifier.MergeModifier(enemyModifier);

            // spawn arena modifiers
            _arenaModifiers = new Dictionary<ValueModifier, GameObject>();
            foreach(ArenaModifierEntry e in arenaModifiers) {
                _arenaModifiers.Add(e.type, e.obj);
            }

            Instantiate(_arenaModifiers[arenaModifier], Vector3.zero, Quaternion.identity, this.gameObject.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                if(player != null && player.GetComponent<Hurtbox>().isAlive == false ) {
                    Debug.Log("aww he ded");
                    GameOver();
                }
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

    public void toCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ToSlotMachine()
    {
        SceneManager.LoadScene("menu_slotmachine");
    }

    public void ToArenaScene(ArenaType arena)
    {
        SceneManager.LoadScene("arena_" + arena.ToString());
    }

    public void ToGameOverScene() {
        instance = null;
        ResetState();
        SceneManager.LoadScene("GameOver");
    }

    public void ToMainMenuScene()
    {
        SceneManager.LoadScene("Menu");
    }

    public bool isArenaPlaying { get { return this.status == GameStatus.running; } }

    public static void ResetState() {
        enemyModifier = ValueModifier.Default();
        playerModifier = ValueModifier.Default();
        arenaModifier = ValueModifier.Default();

        score = 0;
        waveCounter = 1;
    }
}
