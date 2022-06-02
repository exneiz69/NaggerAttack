using UnityEngine;

public class NextWavePanel : HideablePanel
{
    [SerializeField] private WavesSpawner _enemySpawner;

    #region MonoBehaviour

    protected override void OnAwake() { }

    private void OnEnable()
    {
        _enemySpawner.WaveEnded += OnWaveEnded;
    }

    private void OnDisable()
    {
        _enemySpawner.WaveEnded -= OnWaveEnded;
    }

    #endregion

    private void OnWaveEnded(Wave wave)
    {
        if (_enemySpawner.HasNextWave)
        {
            Show();
        }
    }
}
