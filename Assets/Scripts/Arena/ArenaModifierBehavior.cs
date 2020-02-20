using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArenaModifierType {
    none,
    thunderstorm,
    meteor_shower,
}

public abstract class ArenaModifierBehavior : MonoBehaviour
{
    public SunSource sun;

    void Update()
    {
        switch(GameManager.instance.status) {
            case GameStatus.start: UpdatePreGame(); break;
            case GameStatus.running: UpdateGame(); break;
            case GameStatus.end: UpdatePostGame(); break;
        } 
    }

    public abstract void UpdatePreGame();
    public abstract void UpdateGame();
    public abstract void UpdatePostGame();
}
