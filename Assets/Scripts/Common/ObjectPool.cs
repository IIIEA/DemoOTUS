using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
  public class ObjectPool<T> where T : Object
  {
    private const int DEFAULT_POOL_SIZE = 5;

    private readonly Transform _container;
    private readonly Queue<T> _pool;
    private readonly T _prefab;
    private readonly bool _isAutoExpand;

    public ObjectPool(T prefab, int size = DEFAULT_POOL_SIZE, Transform container = null, bool isAutoExpand = true)
    {
      _pool = new Queue<T>(size);
      _prefab = prefab;
      _container = container;
      _isAutoExpand = isAutoExpand;
      
      for (var i = 0; i < size; i++)
        CreateDisabledInstance(_prefab, container);
    }

    public T GetInstance()
    {
      if (_pool.TryDequeue(out T result))
      {
        result.SetActiveState(true);
        return result;
      }
      
      return _isAutoExpand ? CreateDisabledInstance(_prefab, _container) : null;
    }

    public void Release(T instance, Transform container = null)
    {
      if (instance.GetActiveState()) 
        instance.SetActiveState(false);
      
      instance.SetParent(container);
      
      _pool.Enqueue(instance);
    }

    private T CreateDisabledInstance(T prefab, Transform container)
    {
      var instance = Object.Instantiate(prefab);
      instance.SetActiveState(false); 
      instance.SetParent(container);
      _pool.Enqueue(instance);

      return instance;
    }
  }
}