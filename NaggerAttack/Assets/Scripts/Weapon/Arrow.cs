using System;
using UnityEngine;
using UnityEngine.Events;

public class Arrow : MonoBehaviour, IFinishable<Arrow>
{
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _groundLevel;
    [SerializeField] private float _lifeTime;

    private bool _isActive = false;
    private Vector2 _startPoint;
    private Vector2 _controlPoint;
    private Vector2 _endPoint;
    private float _distance;
    private float _interpolant;
    private bool _hit = false;

    public event UnityAction<Arrow> Finished;

    #region MonoBehaviour

    private void OnValidate()
    {
        _damage = _damage < 0 ? 0 : _damage;
        _speed = _speed < 0 ? 0 : _speed;
        _lifeTime = _lifeTime < 0 ? 0 : _lifeTime;
    }

    private void Update()
    {
        if (_isActive)
        {
            if (_interpolant < 1.0f)
            {
                _interpolant += _speed * Time.deltaTime / _distance;
                Vector2 m1 = Vector2.Lerp(_startPoint, _controlPoint, _interpolant);
                Vector2 m2 = Vector2.Lerp(_controlPoint, _endPoint, _interpolant);
                transform.position = Vector2.Lerp(m1, m2, _interpolant);

                var direction = m2 - m1;
                var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                _isActive = false;
                if (_hit)
                {
                    Finish();
                }
                else
                {
                    Timer.Instance.AddWaitingAction(Finish, _lifeTime);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
        {
            if (_isActive)
            {
                enemy.TakeDamage(_damage);
                _hit = true;
            }
        }
    }

    #endregion

    public void Launch(Vector2 target)
    {
        if (CalculatePoints(target))
        {
            _distance = Vector2.Distance(_startPoint, _endPoint);
            _isActive = true;
        }
        else
        {
            throw new ArgumentException();
        }
    }

    public void Restart()
    {
        _isActive = false;
        _startPoint = Vector2.zero;
        _controlPoint = Vector2.zero;
        _endPoint = Vector2.zero;
        _distance = 0f;
        _interpolant = 0f;
        _hit = false;
    }

    private bool CalculatePoints(Vector2 target) // Quadratic Bezier curve
    {
        float t = Mathf.Sqrt((target.y - transform.position.y) / (_groundLevel - transform.position.y));
        if (!float.IsNaN(t))
        {
            _startPoint = transform.position;
            float endPointX = (target.x + t * _startPoint.x - _startPoint.x) / t;
            _endPoint = new Vector2(endPointX, _groundLevel);
            _controlPoint = new Vector2((_startPoint.x + endPointX) / 2.0f, _startPoint.y);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Finish()
    {
        Finished?.Invoke(this);
    }
}
