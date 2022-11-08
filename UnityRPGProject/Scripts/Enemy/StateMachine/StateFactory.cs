using System;
using System.Collections.Generic;

public enum StateType
{
    Idle,
    Chase,
    ChaseStrafe,
    Strafe,
    Combat,
    Patrol,
    Attack,
    Dead
}

public class StateFactory
{
    public StateMachineOwner context;
    public Dictionary<StateType, State> states = new Dictionary<StateType, State>();

    public StateFactory(StateMachineOwner context)
    {
        this.context = context;
        states[StateType.Idle] = SetupIdleState();
        states[StateType.Patrol] = SetupPatrolState();
        states[StateType.Combat] = SetupCombatState();
        states[StateType.Chase] = SetupChaseState();
        states[StateType.ChaseStrafe] = SetupChaseStrafeState();
        states[StateType.Attack] = SetupAttackState();
        states[StateType.Strafe] = SetupStrafeState();
        states[StateType.Dead] = SetupDeadState();
    }


    public State GetState(StateType stateType)
    {
        return states[stateType];
    }

    private State SetupIdleState()
    {
        var state = new State(StateType.Idle);
        state.AddAction(new LookForClosestTargetAction(context));
        return state;
    }

    private State SetupCombatState()
    {
        var state = new State(StateType.Combat);
        return state;
    }

    private State SetupChaseState()
    {
        var state = new State(StateType.Chase);
        state.AddAction(new ChaseTargetAction(context))
            // .AddAction(new LookToTargetAction(context))
            .AddAction(new CombatDicisionAction(context));
        return state;
    }

    private State SetupChaseStrafeState()
    {
        var state = new State(StateType.ChaseStrafe);
        state.AddAction(new ChaseTargetAction(context))
            .AddAction(new LookToTargetAction(context))
            .AddAction(new StrafeAction(context)).
            AddAction(new CombatDicisionAction(context));
        return state;
    }

    private State SetupStrafeState()
    {
        var state = new State(StateType.Strafe)
            .AddAction(new StrafeAction(context))
            .AddAction(new LookToTargetAction(context))
            .AddAction(new CombatDicisionAction(context));
        return state;
    }

    private State SetupAttackState()
    {
        var state = new State(StateType.Attack);
        state.AddAction(new AttackStateAction(context))
            .AddAction(new LookToTargetAction(context));
        return state;
    }

    private State SetupPatrolState()
    {
        var state = new State(StateType.Patrol);
        state.AddAction(new PatrolAction(context))
            // .AddAction(new LookToTargetAction(context))
            .AddAction(new LookForClosestTargetAction(context));
        return state;
    }
    
    private State SetupDeadState()
    {
        var state = new State(StateType.Dead);
        state.AddAction(new DeadStateAction(context));
        return state;
    }
}