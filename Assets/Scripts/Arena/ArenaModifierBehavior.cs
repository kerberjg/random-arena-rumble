using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArenaModifierType {
    none,
    thunderstorm,
    meteor_shower,
}

public struct ArenaModifier {
    public Sprite icon;
    public ArenaModifierType type;

    public static ArenaModifier Default() {
        return new ArenaModifier() {
            icon = null,
            type = ArenaModifierType.none,
        };
    }
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
