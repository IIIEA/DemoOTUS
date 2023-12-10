using System.Collections.Generic;
using Ifrastructure.Events;
using Interface;
using UnityEngine;

namespace Ifrastructure
{
  [RequireComponent(typeof(GameManager))]
  public sealed class GameInstaller : MonoBehaviour
  {
    [SerializeField] private GameEndObserver _gameEndObserver;
    [SerializeField] private StartUpTimerObserver _timerObserver;
    [SerializeField] private GamePauseController _gamePauseController;
    
    private IGameListenersProvider[] _listenerProviders;
    
    private GameManager _gameManager;

    private void Awake()
    {
      _gameManager = GetComponent<GameManager>();
      _listenerProviders = GetComponentsInChildren<IGameListenersProvider>();
      
      foreach (var provider in _listenerProviders)
      {
        IEnumerable<IGameListener> gameListeners = provider.ProvideListeners();

        foreach (var gameListener in gameListeners)
        {
          _gameManager.AddListener(gameListener);
        }
      }
      
      _gameManager.AddListener(_gameEndObserver);
      _gameManager.AddListener(_timerObserver);
      _gameManager.AddListener(_gamePauseController);
    }
  }
}