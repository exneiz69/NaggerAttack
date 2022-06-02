using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class BarView : MonoBehaviour
{
    private Slider _slider;

    protected Slider Slider => _slider;

    #region MonoBehaviour

    protected abstract void OnAwake();

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        OnAwake();
    }

    #endregion

    public virtual void Render(float value, float maxValue) 
        => _slider.value = value / maxValue;
}
