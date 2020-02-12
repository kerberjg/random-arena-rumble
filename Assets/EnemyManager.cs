using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject EnemyContainer;
    public Transform spawnPoint_Enemy;

    public int enemyIncrement;
    public int enemyCountStart;
    int waveEnemies;
    int currentWave = 1;
    int currentEnemies;

    public float spawnDelay;
    float timer_enemySpawner;

    public bool nextWave = true;
    bool ongoingWave = false;

    private void Start()
    {
        waveEnemies = enemyCountStart;
    }

    // Update is called once per frame
    void Update()
    {

        timer_enemySpawner += Time.deltaTime;

        if (nextWave) {

            if(currentEnemies < waveEnemies && timer_enemySpawner >= spawnDelay) {

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
        }

        //IF ongoingWave and nextWave are false we are currently in SlotMachine state.

        if(!ongoingWave && !nextWave) {
            //Do the thing u want before next wave.
            nextWave = true;
            currentWave++;
            waveEnemies = waveEnemies + enemyIncrement;
            
        }
    }
}
