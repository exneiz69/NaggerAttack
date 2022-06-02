using System;
using UnityEngine;

public abstract class Transition<T> : MonoBehaviour, ITransition where T : IState
{
    [SerializeField] private T _targetState;

    public event UnityEngine.Events.UnityAction<ITransition> Finished;

    public IState TargetState => _targetState;

    public bool IsActive => enabled;

    #region MonoBehaviour

    protected abstract void OnAwake();

    private void Awake()
    {
        enabled = false;
        OnAwake();
    }

    #endregion

    public void Enter()
    {
        if (!enabled)
        {
            enabled = true;
            OnEnter();
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public void Exit()
    {
        if (enabled)
        {
            enabled = false;
            OnExit();
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    protected abstract void OnEnter();

    protected abstract void OnExit();

    protected void Finish()
    {
        Exit();
        Finished?.Invoke(this);
    }
}
