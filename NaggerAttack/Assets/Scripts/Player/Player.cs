using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IBeing
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField] private WavesSpawner _spawner;

    private int _money;
    private bool _alive = true;

    public event UnityEngine.Events.UnityAction<IBeing> HealthChanged;

    public event UnityEngine.Events.UnityAction<IBeing> Died;

    public event UnityEngine.Events.UnityAction<Player> MoneyChanged;

    public float Health => _health;

    public float MaxHealth => _maxHealth;

    public int Money => _money;

    #region MonoBehaviour

    private void OnValidate()
    {
        _health = _health < 0 ? 0 : _health;
        _maxHealth = _maxHealth < 0 ? 0 : _maxHealth;
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }


    private void OnEnable()
    {
        _spawner.EnemyDied += OnEnemyDied;
    }

    private void OnDisable()
    {
        _spawner.EnemyDied -= OnEnemyDied;
    }

    private void Start()
    {
        HealthChanged?.Invoke(this);
    }

    #endregion

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

    private void Die()
    {
        Died?.Invoke(this);
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _money += enemy.Reward;
        MoneyChanged?.Invoke(this);
    }
}
