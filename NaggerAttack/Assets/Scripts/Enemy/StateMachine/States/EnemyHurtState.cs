using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyHurtState : EnemyState
{
    [SerializeField] private string _hurtTriggerName;

    private Animator _animator;

    #region MonoBehaviour

    protected override void OnAwake()
    {
        base.OnAwake();
        _animator = GetComponent<Animator>();
    }

    #endregion

    protected override void OnEnter()
    {
        _animator.SetTrigger(_hurtTriggerName);
    }

    protected override void OnExit() { }
}
