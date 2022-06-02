using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bow))]
[RequireComponent(typeof(Animator))]
public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private Vector2 _shootPointOffset;
    [SerializeField] private float _delayAfterShooting;
    [SerializeField] private string _shootingTriggerName;
    [SerializeField, Range(0f, 1f)] private float _arrowLaunchOffset;

    private Bow _bow;
    private Animator _animator;
    private bool _isShooting;
    private WaitWhile _waitWhileNotShooting;
    private WaitForSeconds _waitToArrowLaunch;
    private WaitUntil _waitUntilShooting;
    private WaitForSeconds _waitAfterShooting;

    #region MonoBehaviour

    private void OnValidate()
    {
        _delayAfterShooting = _delayAfterShooting < 0 ? 0 : _delayAfterShooting;
    }

    private void Awake()
    {
        _bow = GetComponent<Bow>();
        _animator = GetComponent<Animator>();
        _waitWhileNotShooting = new WaitWhile(() =>
            _animator.GetCurrentAnimatorStateInfo(0).IsName(_shootingTriggerName));
        _waitUntilShooting = new WaitUntil(() =>
            _animator.GetCurrentAnimatorStateInfo(0).IsName(_shootingTriggerName));
        _waitAfterShooting = new WaitForSeconds(_delayAfterShooting);
    }

    #endregion

    public bool TryShoot(Vector2 target)
    {
        var shootPoint = (Vector3)_shootPointOffset + transform.position;
        if (!_isShooting && target.y < shootPoint.y && target.x > shootPoint.x)
        {
            _isShooting = true;
            StartCoroutine(Shooting(shootPoint, target));
            return true;
        }
        else
        {
            return false;
        }
    }

    private IEnumerator Shooting(Vector2 shootPoint, Vector2 target)
    {
        _animator.SetTrigger(_shootingTriggerName);
        yield return _waitWhileNotShooting;
        yield return _waitToArrowLaunch ??= 
            new WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length * _arrowLaunchOffset);
        _bow.Shoot(shootPoint, target);
        yield return _waitUntilShooting;
        yield return _waitAfterShooting;
        _isShooting = false;
    }
}
