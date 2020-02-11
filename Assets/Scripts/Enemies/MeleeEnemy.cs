using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemyBehavior
{
    [Header("FSM")]
    public string state;

    [Header("Melee behavior")]
    public float circleLeftSpeed = 2f;
    public float circleRightSpeed = 2f;
    public float stabInSpeed = 5f;
    public float stabOutSpeed = 3f;
    public float stabDistance = 0.5f;

    [Header("Melee debug")]
    public float circleAngle = 0f;

    public FinalStateMachineProvider<MeleeEnemy> states;

    public MeleeEnemy() {
        states = new FinalStateMachineProvider<MeleeEnemy>(new Dictionary<string, StateBehavior<MeleeEnemy>> {
            { "circle", new CircleState(this) },
            { "stab_in", new StabInState(this) },
            { "stab_out", new StabOutState(this) },
            { "dead", new DeadState(this) },
        }, "circle");
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
        state = states.state;

        // check health
        if(!GetComponent<Health>().isAlive) {
            states.SwitchState("dead");
        }
    }
}

class CircleState : StateBehavior<MeleeEnemy> {
    private enum CircleDirection {
        left, right
    }

    private CircleDirection direction;

    public float minCircleTime = 1f;
    public float maxCircleTime = 4f;
    public float circleTime;

    public int maxCircles = 4;
    public int circleCount;

    public CircleState(MeleeEnemy m) : base(m) {}
    public override void Mount() {
        // sets movement direction and timing
        ToggleDirection();

        // reset circling counter
        if(circleCount >= maxCircles) {
            circleCount = 0;
        }
    }
    public override void Unmount() {

    }
    public override void Update() {
        // move around in this direction for a definite amount of time
        circleTime -= Time.deltaTime;

        // if movement phase is over...
        if(circleTime <= 0) {
            // ...change direction or move to next phase when counter runs out
            if(++circleCount >= maxCircles) {
                machine.states.SwitchState("stab_in");
            } else {
                ToggleDirection();
            }
        }
        // ...otherwise keep circling in the chosen direction
        else {

            if(this.direction == CircleDirection.left)
                machine.circleAngle += machine.circleLeftSpeed * Mathf.Deg2Rad * Time.deltaTime;
            else
                machine.circleAngle -= machine.circleRightSpeed * Mathf.Deg2Rad * Time.deltaTime;
        }

        // calculate movement target (including circling angle)
        float targetDistance = machine.minTargetDistance + 1f;     // adds a small distance just in case

        machine.enableFollow = false;
        Vector3 actualTargetPos = new Vector3(
            Mathf.Cos(machine.circleAngle),
            Mathf.Sin(machine.circleAngle),
            0f
        ) * targetDistance + machine.targetPos;

        float tmpDistance = machine.minTargetDistance;
        float tmpSpeed = machine.followSpeed;

        machine.minTargetDistance = 0.01f;
        machine.followSpeed = (machine.transform.position - actualTargetPos).magnitude;
        machine.UpdateFollow(actualTargetPos);
        if(machine.stalkerDebugger != null) {
            machine.stalkerDebugger.transform.position = actualTargetPos;
        }

        machine.minTargetDistance = tmpDistance;
        machine.UpdateFollow(machine.targetPos);
    }

    public void ToggleDirection() {
        if(this.direction == CircleDirection.left)
            this.direction = CircleDirection.right;
        else
            this.direction = CircleDirection.left;

        circleTime = Random.Range(minCircleTime, maxCircleTime);
    }
}

abstract class BaseStabState : StateBehavior<MeleeEnemy> {

    protected Vector3 direction;
    protected float distance;

    public BaseStabState(MeleeEnemy m) : base(m) {}

    /// Calculate direction and distance
    public void CalculateDistance() {
        direction = machine.transform.position - machine.targetPos;
        distance = direction.magnitude;
        direction.Normalize();
    }
    
    public Vector3 CalculateMovement(float speed) {
        return direction * speed * Time.deltaTime;
    }
}

class StabInState : BaseStabState {
    public StabInState(MeleeEnemy m) : base(m) {}
    public override void Mount() {}
    public override void Unmount() {}
    public override void Update() {
        CalculateDistance();

        if(distance > machine.stabDistance)
            machine.transform.position = machine.transform.position - CalculateMovement(machine.stabInSpeed);
        else
            machine.states.SwitchState("stab_out");
    }   
}

class StabOutState : BaseStabState {
    public StabOutState(MeleeEnemy m) : base(m) {}
    public override void Mount() {}
    public override void Unmount() {}
    public override void Update() {
        CalculateDistance();

        if(distance < machine.minTargetDistance)
            machine.transform.position = machine.transform.position + CalculateMovement(machine.stabOutSpeed);
        else
            machine.states.SwitchState("circle");
    }   
}

class DeadState : StateBehavior<MeleeEnemy> {
    public DeadState(MeleeEnemy m) : base(m) {}
    public override void Mount() {
        // disable stalking/distancekeeping behavior
        machine.enableFollow = false;
        machine.enableTracking = false;

        // TODO: decide how death state should look like
    }
    public override void Unmount() {}
    public override void Update() {}
    
}

