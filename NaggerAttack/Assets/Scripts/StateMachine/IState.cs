public interface IState
{
    event UnityEngine.Events.UnityAction<IState> Finished;

    bool IsActive { get; }

    void Enter();

    void Exit();
}
