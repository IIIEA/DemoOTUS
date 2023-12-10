using Ifrastructure;
using Ifrastructure.Events;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;

namespace Interface
{
  public class StartUpTimerObserver : SerializedMonoBehaviour,
    IGameFinishListener
  {
    [SerializeField] private TMP_Text _timerText;
    [OdinSerialize] private ITimer _timer;

    private void Awake()
    {
      _timer.OnCountdownStart += OnCountdownStart;
      _timer.OnCountdown += OnCountdown;
      _timer.OnCountdownEnd += OnCountdownEnd;
      _timerText.gameObject.SetActive(false);
    }

    public void OnFinish()
    {
      _timer.OnCountdownStart -= OnCountdownStart;
      _timer.OnCountdown -= OnCountdown;
      _timer.OnCountdownEnd -= OnCountdownEnd;
    }

    private void OnCountdownStart(int timer)
    {
      _timerText.text = timer.ToString();
      _timerText.gameObject.SetActive(true);
    }

    private void OnCountdown(int timer)
    {
      _timerText.text = timer.ToString();
    }

    private void OnCountdownEnd()
    {
      _timerText.gameObject.SetActive(false);
    }
  }
}
