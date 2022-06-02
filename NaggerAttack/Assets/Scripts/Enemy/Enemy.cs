using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(EnemyStateMachine))]
public class Enemy : MonoBehaviour, IBeing, IFinishable<Enemy>
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private int _reward;

    private EnemyStateMachine _enemyStateMachine;
    private Castle _target;
    private bool _alive = true;

    public event UnityAction<IBeing> HealthChanged;
    public event UnityAction<IBeing> Died;
    public event UnityAction<Enemy> Finished;

    public float Health => _health;

    public float MaxHealth => _maxHealth;

    public int Reward => _reward;

    public EnemyStateMachine EnemyStateMachine => _enemyStateMachine;

    public Castle Target => _target;

    #region MonoBehaviour

    private void OnValidate()
    {
        _health = _health < 0 ? 0 : _health;
        _maxHealth = _maxHealth < 0 ? 0 : _maxHealth;
        _reward = _reward < 0 ? 0 : _reward;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }

    private void Awake()
    {
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
    }

    private void Start()
    {
        HealthChanged?.Invoke(this);
    }

    #endregion

    public void Init(Castle target)
    {
        _target = target;
    }

    public void TakeDamage(float damage)
    {
        if (_alive)
        {
            if (_health - damage > 0.0f)
            {
                _health -= damage;
                HealthChanged?.Invoke(this);
            }
            else
            {
                _health = 0.0f;
                HealthChanged?.Invoke(this);
                Die();
            }
        }
        else
        {
            throw new InvalidOperationException();
        }
    }

    public void Restart()
    {
        _health = _maxHealth;
        _target = null;
        _alive = true;
    }

    private void Die()
    {
        _alive = false;
        Died?.Invoke(this);
        Finished?.Invoke(this);
    }
}
