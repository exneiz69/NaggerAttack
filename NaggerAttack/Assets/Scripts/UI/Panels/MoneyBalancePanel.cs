using UnityEngine;

public class MoneyBalancePanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private MoneyBalanceView _moneyBalanceView;

    #region MonoBehaviour

    private void OnEnable()
    {
        _player.MoneyChanged += OnMoneyChanged;
    }

    private void OnDisable()
    {
        _player.MoneyChanged -= OnMoneyChanged;
    }

    #endregion

    private void OnMoneyChanged(Player player) => _moneyBalanceView.Render(player.Money);
}
