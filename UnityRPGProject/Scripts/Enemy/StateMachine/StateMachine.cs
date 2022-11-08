using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : IStateComponent
{
    private Dictionary<State, List<Transition>> transitionTable = new Dictionary<State, List<Transition>>();
    private List<Transition> anyStateTransitions = new List<Transition>();
    public State currentState;

    public void OnUpdate()
    {
        foreach (var transition in anyStateTransitions)
        {
            if (transition.CanTransition())
            {
                SwitchToState(transition.toState);
                break;
            }
        }
        
        if (transitionTable.TryGetValue(currentState, out var transitions))
        {
            foreach (var transition in transitions)
            {
                var canTransition = transition.CanTransition();
                if (canTransition)
                {
                    SwitchToState(transition.toState);
                    break;
                }
            }
        }

        currentState?.OnUpdate();
    }

    public void SwitchToState(State state)
    {
        currentState?.OnStateExit();
        currentState = state;
        currentState.OnStateEnter();
    }

    public void AddTransition(State fromState, State toState, Func<bool> condition)
    {
        var transition = new Transition(toState, condition);
        if (!transitionTable.ContainsKey(fromState))
        {
            transitionTable[fromState] = new List<Transition>();
        }
        transitionTable[fromState].Add(transition);
    }

    public void AddAnyStateTransition(State toState, Func<bool> condition)
    {
        var transition = new Transition(toState, condition);
        anyStateTransitions.Add(transition);

    }

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {

    }
}