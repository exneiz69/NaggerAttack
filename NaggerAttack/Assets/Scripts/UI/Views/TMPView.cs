using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public abstract class TMPView<T> : MonoBehaviour
{
    private TMP_Text _text;

    protected TMP_Text Text => _text;

    #region MonoBehaviour

    protected abstract void OnAwake();

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        OnAwake();
    }

    #endregion

    public abstract void Render(T renderable);
}
