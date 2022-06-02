using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WavesSpawner : MonoBehaviour
{
    [SerializeField] private List<Wave> _waves;

    private Wave _currentWave;
    private int _currentWaveIndex;
    private int _spawnedEnemiesAmount;

    public event UnityAction<Enemy> EnemySpawned;
    public event UnityAction<Enemy> EnemyDied;
    public event UnityAction<Wave> WaveStarted;
    public event UnityAction<Wave> WaveEnded;

    public Wave CurrentWave => _currentWave;

    public bool HasNextWave => _currentWaveIndex + 1 < _waves.Count;

    public int SpawnedEnemiesAmount => _spawnedEnemiesAmount;

    #region MonoBehaviour

    private void Awake()
    {
        ChangeCurrentWave(0);
    }

    private void Start()
    {
        StartCurrentWave();
    }

    #endregion

    public bool TryStartNextWave()
    {
        if (HasNextWave)
        {
            ChangeCurrentWave(_currentWaveIndex + 1);
            StartCurrentWave();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void StartCurrentWave()
    {
        WaveStarted?.Invoke(_currentWave);
        Timer.Instance.AddWaitingAction(SpawnNewEnemy, _currentWave.DelayBeforeSpawn);
    }

    private void SpawnNewEnemy()
    {
        if (_spawnedEnemiesAmount < _currentWave.Amount)
        {
            var enemiesSpawner = _currentWave.EnemiesSpawners[Random.Range(0, _currentWave.EnemiesSpawners.Length)];
            var enemy = enemiesSpawner.Spawn();
            if (enemy is object)
            {
                enemy.Died += OnEnemyDied;
                _spawnedEnemiesAmount++;
                EnemySpawned?.Invoke(enemy);
            }
            Timer.Instance.AddWaitingAction(SpawnNewEnemy, _currentWave.DelayBeforeSpawn);
        }
        else
        {
            WaveEnded?.Invoke(_currentWave);
        }
    }

    private void ChangeCurrentWave(int waveIndex)
    {
        _currentWave = _waves[waveIndex];
        _currentWaveIndex = waveIndex;
        _spawnedEnemiesAmount = 0;
    }

    private void OnEnemyDied(IBeing enemy)
    {
        enemy.Died -= OnEnemyDied;
        EnemyDied?.Invoke(enemy as Enemy);
    }
}

[System.Serializable]
public class Wave
{
    [SerializeField] private EnemiesSpawner[] _enemiesSpawners;
    [SerializeField] private float _delayBeforeSpawn;
    [SerializeField] private int _amount;

    public EnemiesSpawner[] EnemiesSpawners => _enemiesSpawners;

    public float DelayBeforeSpawn => _delayBeforeSpawn;

    public int Amount => _amount;
}