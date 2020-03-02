using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject EnemyContainer;
    public Transform[] spawnPoint_Enemy;
    private int spawnPoint_counter;
    Text waveText;

    public int enemyIncrement = 1;
    public int enemyCountStart = 2;
    public bool isInfinite = false;
    int waveEnemies;
    int currentEnemies;

    public float spawnDelay;
    float timer_enemySpawner;

    float timer_BetweenWaves;

    public bool nextWave = false;
    bool ongoingWave = false;

    public bool killToWin;

    private void Start()
    {
        waveEnemies = GameManager.waveCounter < 2 ? enemyCountStart : GameManager.waveCounter * enemyIncrement;

        waveText = GameObject.Find("WaveText").GetComponent<Text>();
    }

    void Update()
    {
        timer_enemySpawner += Time.deltaTime;

        // level just started, delay start and show text
        if(!ongoingWave && !nextWave) {
            //Do the thing u want before next wave.
            waveText.text = "Wave " + GameManager.waveCounter;
            SoundManager.i.PlayOnce("Cheering", true);
            timer_BetweenWaves += Time.deltaTime;

            // await wave start
            if(timer_BetweenWaves >= GameManager.instance.startWaitTime) {
                nextWave = true;
                spawnPoint_counter = 0;
                timer_BetweenWaves = 0f;
            }
            
        }
        // wave started, spawn enemies, hide text
        else if (nextWave) {

            waveText.enabled = false;

            // keep spawning enemies if necessary
            if ((isInfinite || currentEnemies < waveEnemies) && timer_enemySpawner >= spawnDelay) {

                EnemyContainer.GetComponentInChildren<MeleeEnemy>().target = GameObject.Find("Player").GetComponent<Transform>();
                EnemyContainer.GetComponentInChildren<Hurtbox>().destroyOnDeath = true;
              
                Transform spawnPoint = spawnPoint_Enemy[currentEnemies++ % spawnPoint_Enemy.Length];
                Instantiate(EnemyContainer, spawnPoint.position, spawnPoint.rotation, this.gameObject.transform);    

                timer_enemySpawner = 0f;
            } else if(!isInfinite && currentEnemies == waveEnemies) {
                ongoingWave = true;
                nextWave = false;
            }
        }
        // wave ended, enemies killed, call GameManager and disable spawner
        else if (ongoingWave && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            print("Wave Finished");

            ongoingWave = false;
            currentEnemies = 0;

            GameManager.waveCounter++;
            if (killToWin) {
            GameManager.instance.Win();
            }
        }
    }
}
