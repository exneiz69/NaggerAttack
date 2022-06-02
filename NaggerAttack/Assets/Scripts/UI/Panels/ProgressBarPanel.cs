using UnityEngine;

public class ProgressBarPanel : MonoBehaviour
{
    [SerializeField] private WavesSpawner _spawner;
    [SerializeField] private ProgressBarView _progressBarView;

    #region MonoBehaviour

    private void OnEnable()
    {
        _spawner.EnemySpawned += OnEnemySpawned;
        _spawner.WaveStarted += OnWaveStarted;
    }

    private void OnDisable()
    {
        _spawner.EnemySpawned -= OnEnemySpawned;
        _spawner.WaveStarted -= OnWaveStarted;
    }

    #endregion

    private void OnWaveStarted(Wave wave) 
        => _progressBarView.Render(_spawner.SpawnedEnemiesAmount, wave.Amount);

    private void OnEnemySpawned(Enemy enemy)
        => _progressBarView.Render(_spawner.SpawnedEnemiesAmount, _spawner.CurrentWave.Amount);
}
