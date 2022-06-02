using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class State<T> : MonoBehaviour, IState where T : ITransition
{
    [SerializeField] private List<T> _transitions;

    public event UnityEngine.Events.UnityAction<IState> Finished;

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
            foreach (var transition in _transitions)
            {
                transition.Enter();
                transition.Finished += OnTransitionFinish;
            }
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
            foreach (var transition in _transitions)
            {
                if (transition.IsActive)
                {
                    transition.Exit();
                }
                transition.Finished -= OnTransitionFinish;
            }
            OnExit();
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    protected abstract void OnEnter();

    protected abstract void OnExit();

    private void OnTransitionFinish(ITransition transition)
    {
        Exit();
        Finished?.Invoke(transition.TargetState);
    }
}