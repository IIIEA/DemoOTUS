using System.Collections.Generic;
using Ifrastructure.Events;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Ifrastructure
{
  public enum GameState
  {
    NOT_READY,
    GAMELOOP,
    FINISH,
    PAUSE
  }

  public sealed class GameManager : MonoBehaviour
  {
    [SerializeField, ReadOnly] private GameState _gameState = GameState.NOT_READY;

    private List<IGameListener> _gameListeners = new();
    private List<IGameUpdateListener> _gameUpdateListeners = new();
    private List<IGameFixedUpdateListener> _gameFixedUpdateListeners = new();

    public GameState CurrentGameState => _gameState;
    
    public void AddListener(IGameListener gameListener)
    {
      _gameListeners.Add(gameListener);

      if (gameListener is IGameUpdateListener updateListener)
      {
        _gameUpdateListeners.Add(updateListener);
      }

      if (gameListener is IGameFixedUpdateListener fixedUpdateListener)
      {
        _gameFixedUpdateListeners.Add(fixedUpdateListener);
      }
    }

    public void AddListeners(IGameListener[] gameListeners)
    {
      foreach (var gameListener in gameListeners)
      {
        AddListener(gameListener);
      }
    }

    public void RemoveListeners(IGameListener[] gameListeners)
    {
      foreach (var gameListener in gameListeners)
      {
        RemoveListener(gameListener);
      }
    }

    public void RemoveListener(IGameListener gameListener)
    {
      if (gameListener == null)
        return;

      _gameListeners.Remove(gameListener);

      if (gameListener is IGameUpdateListener updateListener)
      {
        _gameUpdateListeners.Remove(updateListener);
      }

      if (gameListener is IGameFixedUpdateListener fixedUpdateListener)
      {
        _gameFixedUpdateListeners.Remove(fixedUpdateListener);
      }
    }

    [Button]
    public void StartGame()
    {
      foreach (var listener in _gameListeners)
      {
        if (listener is IGameStartListener gameStartListener)
        {
          gameStartListener.OnStart();
        }
      }

      _gameState = GameState.GAMELOOP;
    }
    
    [Button]
    public void PauseGame()
    {
      foreach (var listener in _gameListeners)
      {
        if (listener is IGamePauseListener gamePauseListener)
        {
          gamePauseListener.OnPause();
        }
      }

      _gameState = GameState.PAUSE;
    }

    [Button]
    public void ResumeGame()
    {
      foreach (var listener in _gameListeners)
      {
        if (listener is IGameResumeListener gameResumeListener)
        {
          gameResumeListener.OnResume();
        }
      }

      _gameState = GameState.GAMELOOP;
    }

    [Button]
    public void FinishGame()
    {
      foreach (var listener in _gameListeners)
      {
        if (listener is IGameFinishListener gameFinishListener)
        {
          gameFinishListener.OnFinish();
        }
      }

      _gameState = GameState.FINISH;
      Debug.Log("Game over!");
      // Time.timeScale = 0;
    }

    private void Update()
    {
      for (int i = 0; i < _gameUpdateListeners.Count; i++)
      {
        _gameUpdateListeners[i].OnUpdate(Time.deltaTime);
      }
    }

    private void FixedUpdate()
    {
      for (int i = 0; i < _gameFixedUpdateListeners.Count; i++)
      {
        _gameFixedUpdateListeners[i].OnFixedUpdate(Time.fixedDeltaTime);
      }
    }
  }
}