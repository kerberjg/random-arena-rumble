using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalStateMachineProvider<T> where T: MonoBehaviour {
    private Dictionary<string, StateBehavior<T>> states;
    public string state { get; private set; }
    private string _nextState;

    public FinalStateMachineProvider(Dictionary<string, StateBehavior<T>> statesMap, string firstState) {
        this.states = statesMap;
        SwitchState(firstState);
    }

    // Update is called once per frame
    public void UpdateState()
    {
        StateBehavior<T> currentState;
        states.TryGetValue(state, out currentState);

        if(_nextState != null) {
            // unmount old state
            if(currentState != null) {
                currentState.Unmount();
            }
            
            // mount new state
            state = _nextState;
            _nextState = null;
            states.TryGetValue(state, out currentState);
            currentState.Mount();
        }

        currentState.Update();
    }

    public void SwitchState(string stateName) {
        if(states.ContainsKey(stateName)) {
            _nextState = stateName;
        } else {
            throw new FiniteStateMachineException("No such state: " + stateName);
        }
    }
}

public abstract class StateBehavior<B> where B: MonoBehaviour {
    protected readonly B machine;
    public StateBehavior(B machine) {
        this.machine = machine;
    }

    abstract public void Mount();
    abstract public void Unmount();
    abstract public void Update();
}

public class FiniteStateMachineException : System.Exception {
    public FiniteStateMachineException(string s) : base(s) {}
}