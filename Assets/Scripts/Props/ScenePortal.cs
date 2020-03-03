using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class ScenePortal : MonoBehaviour
{
    public string scene;

    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == "Player") {
            SceneManager.LoadScene(scene);
        }
    }
}