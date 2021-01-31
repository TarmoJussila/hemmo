using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum StateType
{
    Start = 0,
    Game = 1,
    Finish = 2,
    Pause = 3,
}

[System.Serializable]
public class State
{
    public StateType StateType;
    public UnityEvent OnStateEnterEvent;
}

public class StateManager : MonoBehaviour
{
    public StateType CurrentState { get; private set; }

    [SerializeField] private List<State> states;

    private void Awake()
    {
        ChangeState(StateType.Start);
    }

    public void ChangeState(StateType stateType)
    {
        CurrentState = stateType;
        var state = states.Find(s => s.StateType == stateType);
        state.OnStateEnterEvent?.Invoke();
    }
}