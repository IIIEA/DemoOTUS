using Ifrastructure.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ifrastructure
{
  public class GamePauseController : MonoBehaviour,
    IGameStartListener,
    IGameFinishListener
  {
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private TMP_Text _buttonText;

    private const string RESUME = "Resume";
    private const string PAUSE = "Pause";
    
    private void Awake()
    {
      _pauseButton.gameObject.SetActive(false);
    }

    public void OnStart()
    {
      _buttonText.text = PAUSE;
      _pauseButton.gameObject.SetActive(true);
      _pauseButton.onClick.AddListener(OnPauseButtonClick);
    }

    public void OnFinish()
    {
      _pauseButton.gameObject.SetActive(false);
      _pauseButton.onClick.RemoveListener(OnPauseButtonClick);
    }

    private void OnPauseButtonClick()
    {
      switch (_gameManager.CurrentGameState)
      {
        case GameState.GAMELOOP:
          _gameManager.PauseGame();
          _buttonText.text = RESUME;
          break;
        case GameState.PAUSE:
          _gameManager.ResumeGame();
          _buttonText.text = PAUSE;
          break;
      }
    }
  }
}