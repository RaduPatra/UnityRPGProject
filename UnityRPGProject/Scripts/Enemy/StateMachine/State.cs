
    using System.Collections.Generic;
    using UnityEngine;

    public class State: IStateComponent
    {
        public List<StateAction> actions = new List<StateAction>();
        public StateType stateType;

        public State(StateType stateType)
        {
            this.stateType = stateType;
        }

        public void  OnUpdate()
        {
            foreach (var action in actions)
            {
                action.OnUpdate();
            }
        }

        public void OnStateEnter()
        {
            foreach (var action in actions)
            {
                action.OnStateEnter();
            }
        }

        public void OnStateExit()
        {
            foreach (var action in actions)
            {
                action.OnStateExit();
            }
        }

        public State AddAction(StateAction action)
        {
            actions.Add(action);
            return this;
        }
    }
