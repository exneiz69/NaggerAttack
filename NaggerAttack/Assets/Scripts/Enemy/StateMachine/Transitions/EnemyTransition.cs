using System;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyTransition : Transition<EnemyState>
{
    protected Enemy Enemy { get; private set; }

    #region MonoBehaviour

    protected override void OnAwake()
    {
        Enemy = GetComponent<Enemy>();
    }

    #endregion
}
