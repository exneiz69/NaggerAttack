using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public abstract class HideablePanel : MonoBehaviour
{
    [SerializeField] private bool hideOnAwake = true;

    private CanvasGroup _panel;

    #region MonoBehaviour

    protected abstract void OnAwake();

    private void Awake()
    {
        _panel = GetComponent<CanvasGroup>();
        if (hideOnAwake)
        {
            Hide();
        }
        else
        {
            Show();
        }
        OnAwake();
    }

    #endregion

    public void Show()
    {
        _panel.alpha = 1;
        _panel.blocksRaycasts = true;
    }

    public void Hide()
    {
        _panel.alpha = 0;
        _panel.blocksRaycasts = false;
    }
}
