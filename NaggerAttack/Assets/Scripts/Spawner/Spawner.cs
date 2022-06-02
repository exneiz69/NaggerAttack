using System;
using UnityEngine;

public abstract class Spawner<T> : MonoBehaviour where T : Component, IFinishable<T>
{
    [SerializeField] private T _prefab;
    [SerializeField] private GameObject _poolContainer;
    [SerializeField] private int _poolCapacity;

    protected ObjectPool<T> ObjectPool => _objectPool;

    private ObjectPool<T> _objectPool;

    #region MonoBehaviour

    protected virtual void OnValidate()
    {
        _poolCapacity = _poolCapacity < 0 ? 0 : _poolCapacity;
    }

    protected abstract void OnAwake();

    private void Awake()
    {
        _objectPool = new ObjectPool<T>(_poolContainer, _poolCapacity, _prefab);
        OnAwake();
    }

    #endregion

    protected abstract bool CheckSpawnAvailability();

    protected abstract void Prepare(T spawnObject);

    public T Spawn()
    {
        if (CheckSpawnAvailability())
        {
            if (_objectPool.TryGetObject(out T spawnObject))
            {
                Prepare(spawnObject);
                return spawnObject;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return null;
        }
    }
}