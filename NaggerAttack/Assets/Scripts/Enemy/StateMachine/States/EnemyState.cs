using UnityEngine;

[RequireComponent(typeof(Enemy))]
public abstract class EnemyState : State<EnemyTransition>
{
    protected Enemy Enemy { get; private set; }

    #region MonoBehaviour

    protected override void OnAwake()
    {
        Enemy = GetComponent<Enemy>();
    }

    #endregion
}
