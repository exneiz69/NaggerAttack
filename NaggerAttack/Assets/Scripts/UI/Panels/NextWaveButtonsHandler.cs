using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(NextWavePanel))]
public class NextWaveButtonsHandler : MonoBehaviour
{
    [SerializeField] private Button _nextWaveButton;
    [SerializeField] private WavesSpawner _enemySpawner;

    private NextWavePanel _nextWavePanel;

    #region MonoBehaviour

    private void Awake()
    {
        _nextWavePanel = GetComponent<NextWavePanel>();
    }

    private void OnEnable()
    {
        _nextWaveButton.onClick.AddListener(OnNextWaveButtonClick);
    }

    private void OnDisable()
    {
        _nextWaveButton.onClick.RemoveListener(OnNextWaveButtonClick);
    }

    #endregion

    private void OnNextWaveButtonClick()
    {
        _nextWavePanel.Hide();
        _enemySpawner.TryStartNextWave();
    }
}
