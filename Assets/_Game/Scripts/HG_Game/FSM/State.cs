namespace HG
{
    public abstract class State<T> 
    {
        protected T _owner;
        protected Fsm<T> _fsm;

        public virtual State<T> SetState(Fsm<T> theStateMachine, T theOnwer)
        {
            _fsm = theStateMachine;
            _owner = theOnwer;
            return this;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void LateUpdate() { }
        public virtual void FixedUpdate() { }
        public virtual void Exit() { }
    }
}


