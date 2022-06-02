using UnityEngine;

public class EnemiesSpawner : Spawner<Enemy>
{
    [SerializeField] private Castle _enemyTarget;
    [SerializeField] private Transform _spawnPoint;

    #region MonoBehaviour

    protected override void OnAwake() { }

    #endregion

    protected override bool CheckSpawnAvailability() => true;

    protected override void Prepare(Enemy enemy)
    {
        enemy.transform.position = _spawnPoint.position;
        enemy.gameObject.SetActive(true);
        enemy.Init(_enemyTarget);
        enemy.EnemyStateMachine.Reload();
    }
}
