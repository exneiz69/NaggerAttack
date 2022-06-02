using UnityEngine;
using UnityEngine.Events;
using Priority_Queue;
using System;

public class Timer : MonoBehaviourSingleton<Timer>, ISingleMonoBehaviour
{
    private SimplePriorityQueue<UnityAction> _actions = new SimplePriorityQueue<UnityAction>();
    private float _elapsedTime = 0;

    #region MonoBehaviour

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        while (_actions.Count != 0 && _actions.GetPriority(_actions.First) < _elapsedTime)
        {
            _actions.Dequeue()?.Invoke();
        }
    }

    #endregion

    public void AddWaitingAction(UnityAction action, float waitingTime)
    {
        if (action == null)
        {
            throw new ArgumentNullException();
        }
        else if (waitingTime < 0)
        {
            throw new ArgumentOutOfRangeException();
        }
        else
        {
            bool withoutDuplicates = _actions.EnqueueWithoutDuplicates(action, waitingTime + _elapsedTime);
            if (!withoutDuplicates)
            {
                throw new ArgumentException();
            }
        }
    }

    public void RemoveWaitingAction(UnityAction action)
    {
        if (action == null)
        {
            throw new ArgumentNullException();
        }
        else
        {
            _actions.Remove(action);
        }
    }

    public bool TryRemoveWaitingAction(UnityAction action)
    {
        if (action == null)
        {
            throw new ArgumentNullException();
        }
        else
        {
            return _actions.TryRemove(action);
        }
    }
}
