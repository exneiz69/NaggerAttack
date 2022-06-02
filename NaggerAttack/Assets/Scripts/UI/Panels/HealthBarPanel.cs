using UnityEngine;

public class HealthBarPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private HealthBarView _healthBarView;

    #region MonoBehaviour

    private void OnEnable()
    {
        _player.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _player.HealthChanged -= OnHealthChanged;
    }

    #endregion

    private void OnHealthChanged(IBeing player)
    {
        _healthBarView.Render(player.Health, player.MaxHealth);
    }
}
