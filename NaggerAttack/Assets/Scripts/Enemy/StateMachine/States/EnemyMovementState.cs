using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyMovementState : EnemyState
{
    [SerializeField] private float _speed;
    [SerializeField] private string _runTriggerName;

    private Animator _animator;

    #region MonoBehaviour

    private void OnValidate()
    {
        _speed = _speed < 0 ? 0 : _speed;
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (IsActive)
        {
            transform.position = Vector2.MoveTowards(transform.position, Enemy.Target.transform.position, _speed * Time.deltaTime);
        }
    }

    #endregion

    protected override void OnEnter()
    {
        _animator.SetTrigger(_runTriggerName);
    }

    protected override void OnExit() { }
}
