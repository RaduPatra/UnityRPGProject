
    using System;

    public class Transition
    {
        public State toState;
        public Func<bool> condition;

        public Transition(State toState, Func<bool> condition)
        {
            this.toState = toState;
            this.condition = condition;
        }

        public bool CanTransition()
        {
            return condition.Invoke();
        }
    }
