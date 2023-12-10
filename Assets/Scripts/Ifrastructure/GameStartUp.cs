using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Ifrastructure
{
  public sealed class GameStartUp : MonoBehaviour, ITimer
  {
    [SerializeField] private int _delay = 3;
    [SerializeField] private int _countdown = 1;
    [SerializeField] private Button _startGameButton;
    [SerializeField] private GameManager _gameManager;

    private WaitForSeconds _coroutineDelay;
    private int _currentTimer;

    private void Awake()
    {
      _coroutineDelay = new WaitForSeconds(_countdown);
    }

    public event Action<int> OnCountdown;
    public event Action<int> OnCountdownStart;
    public event Action OnCountdownEnd;

    private void OnEnable()
    {
      _startGameButton.onClick.AddListener(StartUp);
    }

    private void OnDisable()
    {
      _startGameButton.onClick.RemoveListener(StartUp);
    }

    private void StartUp()
    {
      StartCoroutine(StartUpRoutine());
    }

    private IEnumerator StartUpRoutine()
    {
      _startGameButton.gameObject.SetActive(false);
      _currentTimer = _delay;
      OnCountdownStart?.Invoke(_currentTimer);
      
      while (_currentTimer > 0)
      {
        _currentTimer--;
        
        OnCountdown?.Invoke(_currentTimer);

        yield return _coroutineDelay;
      }
    
      OnCountdownEnd?.Invoke();
      _gameManager.StartGame();
    }
  }
}