using System.Collections.Generic;
using Ifrastructure.Events;
using ShootEmUp;
using UnityEngine;

namespace Ifrastructure.Providers
{
  public class BulletInstaller : MonoBehaviour, 
    IGameListenersProvider
  {
    [SerializeField] private BulletSystem _bulletSystem;
    
    public IEnumerable<IGameListener> ProvideListeners()
    {
      yield return _bulletSystem;
    }
  }
}