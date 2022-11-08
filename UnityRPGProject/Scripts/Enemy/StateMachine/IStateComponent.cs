
    using UnityEngine;

    public interface IStateComponent
    {
        public void OnUpdate();
        public void OnStateEnter();
        public void OnStateExit();
    }
