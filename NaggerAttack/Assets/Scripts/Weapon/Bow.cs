using UnityEngine;

public class Bow : MonoBehaviour
{
    [SerializeField] protected Arrow Prefab;
    [SerializeField] private GameObject _arrowsContainer;
    [SerializeField] private int _arrowsCount;

    private ObjectPool<Arrow> _arrowsPool;

    #region MonoBehaviour

    private void OnValidate()
    {
        _arrowsCount = _arrowsCount < 0 ? 0 : _arrowsCount;
    }

    private void Awake()
    {
        _arrowsPool = new ObjectPool<Arrow>(_arrowsContainer, _arrowsCount, Prefab);
    }

    #endregion  

    public virtual void Shoot(Vector2 shootPoint, Vector2 target)
    {
        if (_arrowsPool.TryGetObject(out Arrow arrow))
        {
            arrow.transform.position = shootPoint;
            arrow.transform.rotation = Quaternion.identity;
            arrow.gameObject.SetActive(true);
            arrow.Launch(target);
        }
    }    
}
