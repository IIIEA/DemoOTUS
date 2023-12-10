using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ShootEmUp
{
  [Serializable]
  public abstract class Factory<T> : MonoBehaviour where T : Object
  {
    [Header("Factory")]
    [SerializeField] private T _prefab;
    [SerializeField] private Transform _container;
    [SerializeField] private int _initialCapacity;
    [SerializeField] private bool _isAutoExpand;

    protected readonly HashSet<T> _activeObjects = new();
    private ObjectPool<T> _pool;

    public IReadOnlyCollection<T> ActiveObjects => _activeObjects;

    private void Awake()
    {
      _pool = new ObjectPool<T>(_prefab, _initialCapacity, _container, _isAutoExpand);
    }

    protected bool TryGetInstance(out T instance)
    {
      instance = _pool.GetInstance();

      if (instance)
      {
        _activeObjects.Add(instance);
      }

      return instance;
    }

    public virtual bool ReleaseInstance(T instance)
    {
      if (instance)
      {
        _pool.Release(instance, _container);
      }

      return _activeObjects.Remove(instance);
    }
  }
}