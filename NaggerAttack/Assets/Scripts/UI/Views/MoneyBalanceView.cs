public class MoneyBalanceView : TMPView<int>
{
    #region MonoBehaviour

    protected override void OnAwake() { }

    #endregion

    public override void Render(int moneyBalance) 
        => Text.text = moneyBalance.ToString();
}
