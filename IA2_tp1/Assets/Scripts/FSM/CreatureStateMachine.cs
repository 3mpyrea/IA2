using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class CreatureStateMachine 
{
    States _currentState;

    Dictionary<CreatureStates, States> _allStates = new Dictionary<CreatureStates, States>();


    public void Update()
    {
        _currentState?.OnUpdate();
    }

    public void AddState(CreatureStates name, States state)
    {
        if (!_allStates.ContainsKey(name))
        {
            _allStates.Add(name, state);
            state.fsm = this;
        }
        else
        {
            _allStates[name] = state;
        }

    }
    public void ChangeState(CreatureStates name)
    {
        if (_currentState != _allStates[name])
        {
            _currentState?.OnExit();
            if (_allStates.ContainsKey(name)) { _currentState = _allStates[name]; }
            _currentState.OnEnter();
        }
        else { return; }
    }

    public enum CreatureStates
    {
        seekResource, consumeResource, rest
    }
}
