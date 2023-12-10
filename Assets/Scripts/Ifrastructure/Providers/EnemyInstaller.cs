using System.Collections.Generic;
using GamePlay.Enemy;
using Ifrastructure.Events;
using UnityEngine;

namespace Ifrastructure.Providers
{
  public class EnemyInstaller : MonoBehaviour, 
    IGameListenersProvider
  {
    [SerializeField] private EnemySpawner _enemySpawner;

    public IEnumerable<IGameListener> ProvideListeners()
    {
      yield return _enemySpawner;
    }
  }
}