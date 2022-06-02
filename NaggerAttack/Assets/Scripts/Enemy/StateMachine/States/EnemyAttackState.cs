using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyAttackState : EnemyState
{
    [SerializeField] private int _damage;
    [SerializeField] private float _delay;
    [SerializeField] private string _attackTriggerName;

    private Animator _animator;

    #region MonoBehaviour

    private void OnValidate()
    {
        _damage = _damage < 0 ? 0 : _damage;
        _delay = _delay < 0 ? 0 : _delay;
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        _animator = GetComponent<Animator>();
    }

    #endregion

    protected override void OnEnter()
    {
        Attack();
    }

    protected override void OnExit()
    {
        Timer.Instance.TryRemoveWaitingAction(Attack);
    }

    private void Attack()
    {
        _animator.SetTrigger(_attackTriggerName);
        Enemy.Target.TakeDamage(_damage);
        Timer.Instance.AddWaitingAction(Attack, _delay);
    }
}
