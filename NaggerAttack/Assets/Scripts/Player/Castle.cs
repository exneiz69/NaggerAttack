using System;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField] private float _health;
    [SerializeField] private float _maxHealth;
    [SerializeField, Range(0f, 1f)] private float _protection;
    [SerializeField] private Player _player;

    private bool _isDestroyed = false;

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

    #endregion

    public void TakeDamage(float damage)
    {
        if (_isDestroyed)
        {
            _player.TakeDamage(damage);
        }
        else
        {
            _player.TakeDamage(damage - damage * _protection);
            if (_health - damage > 0.0f)
            {
                _health -= damage;
            }
            else
            {
                _health = 0.0f;
                Destroy();
            }
        }
    }

    private void Destroy()
    {
        _isDestroyed = true;
    }
}
