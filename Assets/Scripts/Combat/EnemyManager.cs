using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public GameObject EnemyContainer;
    public Transform spawnPoint_Enemy;
    Text waveText;

    public int enemyIncrement;
    public int enemyCountStart;
    int waveEnemies;
    int currentWave = 1;
    int currentEnemies;

    public float spawnDelay;
    float timer_enemySpawner;

    public float waveDelay;
    float timer_BetweenWaves;

    public bool nextWave = false;
    bool ongoingWave = false;

    private void Start()
    {
        waveEnemies = enemyCountStart;

        waveText = GameObject.Find("WaveText").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        timer_enemySpawner += Time.deltaTime;

        if (nextWave) {

            waveText.enabled = false;

            if (currentEnemies < waveEnemies && timer_enemySpawner >= spawnDelay) {

                EnemyContainer.GetComponentInChildren<MeleeEnemy>().target = GameObject.Find("Player").GetComponent<Transform>();
                EnemyContainer.GetComponentInChildren<Hurtbox>().destroyOnDeath = true;
              
                Instantiate(EnemyContainer, spawnPoint_Enemy.position, spawnPoint_Enemy.rotation);    

                currentEnemies++;

                timer_enemySpawner = 0f;
            } else if(currentEnemies == waveEnemies) {
                ongoingWave = true;
                nextWave = false;
            }

            
        }

        if (ongoingWave && GameObject.FindGameObjectsWithTag("Enemy").Length == 0) {
            print("Wave Finished");
            ongoingWave = false;
            currentEnemies = 0;

            waveText.enabled = true;
            currentWave++;
            waveText.text = "Wave " + currentWave;
            SoundManager.i.PlayOnce("Cheering");

        }

        //IF ongoingWave and nextWave are false we are currently in SlotMachine state.

        if(!ongoingWave && !nextWave) {
            //Do the thing u want before next wave.
            timer_BetweenWaves += Time.deltaTime;

            if(timer_BetweenWaves >= waveDelay) {
             
                nextWave = true;
                
                waveEnemies = waveEnemies + enemyIncrement;

                timer_BetweenWaves = 0f;
                
            }
            
        }
    }
}
