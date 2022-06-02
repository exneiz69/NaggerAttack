using System;
using UnityEngine;

public class EnemyStunTransition : EnemyTransition
{
    [SerializeField] private float _stunTime;

    #region MonoBehaviour

    private void OnValidate()
    {
        _stunTime = _stunTime < 0 ? 0 : _stunTime;
    }

    #endregion

    protected override void OnEnter()
    {
        Timer.Instance.AddWaitingAction(Finish, _stunTime);
    }

    protected override void OnExit()
    {
        Timer.Instance.TryRemoveWaitingAction(Finish);
    }
}
