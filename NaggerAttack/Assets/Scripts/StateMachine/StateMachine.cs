using UnityEngine;

public abstract class StateMachine<T> : MonoBehaviour where T : class, IState
{
    [SerializeField] private T _initialState;

    private T _currentState;

    public void Reload()
    {
        Transit(_initialState);
    }

    private void Transit(T nextState)
    {
        if (_currentState is object)
        {
            if (_currentState.IsActive)
            {
                _currentState.Exit();
            }
            _currentState.Finished -= OnStateFinish;
        }
        _currentState = nextState;
        _currentState.Enter();
        _currentState.Finished += OnStateFinish;
    }

    private void OnStateFinish(IState nextState)
    {
        Transit(nextState as T);
    }
}