using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> where T : Component, IFinishable<T>
{
    private GameObject _container;
    private int _capacity;
    private T _prefab;

    private LinkedList<T> _pool;

    public ObjectPool(GameObject container, int capacity, T prefab)
    {
        _container = container;
        _capacity = capacity;
        _prefab = prefab;
        Fill();
    }

    public bool TryGetObject(out T result)
    {
        if (_pool.Count != 0)
        {
            result = _pool.First.Value;
            _pool.RemoveFirst();
            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }

    private void Fill()
    {
        _pool = new LinkedList<T>();
        for (int i = 0; i < _capacity; i++)
        {
            T newComponent = GameObject.Instantiate(_prefab, _container.transform);
            newComponent.name = $"{_prefab.name} {i}";
            newComponent.gameObject.SetActive(false);
            newComponent.Finished += OnFinished;
            _pool.AddLast(newComponent);
        }
    }

    private void OnFinished(T component)
    {
        component.gameObject.SetActive(false);
        component.Restart();
        _pool.AddLast(component);
    }
}

public interface IFinishable<T> where T : Component
{
    event UnityEngine.Events.UnityAction<T> Finished;

    void Restart();
}