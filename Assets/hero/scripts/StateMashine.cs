public class StateMashine
{
    public State CurrentState { get; private set; }

    public void SetState(State state)
    {
        CurrentState?.Exit();
        CurrentState = state;
        CurrentState?.Enter();
    }

}
