using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlocker : MonoBehaviour
{

    public GameObject enemyBlocker;
    public GameObject enemyManger;
  

    // Update is called once per frame
    void Update()
    {
        if(enemyBlocker == null) {
            enemyManger.SetActive(true);
        }
    }
}
