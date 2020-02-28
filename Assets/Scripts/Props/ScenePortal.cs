using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour
{
    public Scene scene;

    void OnTriggerCollision2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player") {
            SceneManager.LoadScene(scene.name);
        }
    }
}