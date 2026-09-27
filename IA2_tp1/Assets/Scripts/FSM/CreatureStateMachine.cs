using System.Collections.Generic;
using UnityEngine;

public class CreatureStateMachine
{
    public enum CreatureStates
    {
        seekResource, consumeResource, rest, roam
    }

    private States _currentState;
    private Dictionary<CreatureStates, States> _allStates = new Dictionary<CreatureStates, States>();

    public void Update()
    {
        _currentState?.OnUpdate();
    }

    public void AddState(CreatureStates name, States state)
    {
        if (!_allStates.ContainsKey(name))
        {
            _allStates.Add(name, state);
        }
        else
        {
            _allStates[name] = state;
        }
    }

    public void ChangeState(CreatureStates name)
    {
        if (_currentState == _allStates[name]) return;

        _currentState?.OnExit();
        _currentState = _allStates[name];
        _currentState.OnEnter();
    }
}
