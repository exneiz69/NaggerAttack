using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraScaler : MonoBehaviour
{
    [SerializeField] private float _boxWidth;

    #region MonoBehaviour

    private void Start()
    {
        GetComponent<Camera>().orthographicSize = _boxWidth * Screen.height / Screen.width * 0.5f;
    }

    #endregion
}
