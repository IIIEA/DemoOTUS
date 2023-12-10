using System.Collections.Generic;
using Ifrastructure.Events;
using ShootEmUp;
using UnityEngine;

namespace Ifrastructure.Providers
{
  public class LevelInstaller : MonoBehaviour, 
    IGameListenersProvider
  {
    [SerializeField] private LevelBackground _levelBackground;
    
    public IEnumerable<IGameListener> ProvideListeners()
    {
      yield return _levelBackground;
    }
  }
}