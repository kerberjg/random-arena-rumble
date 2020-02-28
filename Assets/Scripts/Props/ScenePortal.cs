using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ScenePortal : MonoBehaviour
{
    public SceneAsset scene;

    void OnTriggerCollision2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player") {
            SceneManager.LoadScene(scene.name);
        }
    }
}