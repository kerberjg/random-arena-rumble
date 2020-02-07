using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemyBehavior
{
    public string state { get { return states.state; } }
    
    [Header("Melee behavior")]public float circleSpeed = 2f;
    public float stabInSpeed = 5f;
    public float stabOutSpeed = 3f;

    private FinalStateMachineProvider<MeleeEnemy> states;

    public MeleeEnemy() {
        states = new FinalStateMachineProvider<MeleeEnemy>(new Dictionary<string, StateBehavior<MeleeEnemy>> {
            { "circle_left", new CircleLeftState(this) },
            { "circle_right", new CircleRightState(this) },
            { "stab_in", new StabInState(this) },
            { "stab_out", new StabOutState(this) },
        }, "circle_left");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void EnemyUpdate()
    {
        // update FSM
        states.UpdateState();
    }
}

class CircleLeftState : StateBehavior<MeleeEnemy> {
    public CircleLeftState(MeleeEnemy m) : base(m) {}
    public override void Mount() {}
    public override void Unmount() {}
    public override void Update() {}
}

class CircleRightState : StateBehavior<MeleeEnemy> {
    public CircleRightState(MeleeEnemy m) : base(m) {}
    public override void Mount() {}
    public override void Unmount() {}
    public override void Update() {}
    
}

class StabInState : StateBehavior<MeleeEnemy> {
    public StabInState(MeleeEnemy m) : base(m) {}
    public override void Mount() {}
    public override void Unmount() {}
    public override void Update() {}
    
}

class StabOutState : StateBehavior<MeleeEnemy> {
    public StabOutState(MeleeEnemy m) : base(m) {}
    public override void Mount() {}
    public override void Unmount() {}
    public override void Update() {}
    
}

