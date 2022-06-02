using UnityEngine;

public class EnemyDistanceTransition : EnemyTransition
{
    [SerializeField] private float _targetMaxDistance;
    [SerializeField] private float _distanceDeviation;

    private float _currentTargetMaxDistance;

    #region MonoBehaviour

    private void OnValidate()
    {
        _targetMaxDistance = _targetMaxDistance < 0 ? 0 : _targetMaxDistance;
        _distanceDeviation = _distanceDeviation < 0 ? 0 : _distanceDeviation;
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        _currentTargetMaxDistance = _targetMaxDistance;
        _currentTargetMaxDistance += Random.Range(-_distanceDeviation, _distanceDeviation);
    }

    private void Update()
    {
        if (IsActive)
        {
            if (Vector2.Distance(transform.position, Enemy.Target.transform.position) <= _currentTargetMaxDistance)
            {
                Finish();
            }
        }
    }

    #endregion

    protected override void OnEnter() { }

    protected override void OnExit() { }
}
