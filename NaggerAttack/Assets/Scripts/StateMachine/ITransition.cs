public interface ITransition
{
    event UnityEngine.Events.UnityAction<ITransition> Finished;

    IState TargetState { get; }

    bool IsActive { get; }

    void Enter();

    void Exit();
}
