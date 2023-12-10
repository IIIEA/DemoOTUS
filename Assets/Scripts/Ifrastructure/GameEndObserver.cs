using Ifrastructure.Events;
using ShootEmUp;
using UnityEngine;

namespace Ifrastructure
{
  public class GameEndObserver : MonoBehaviour,
    IGameStartListener,
    IGameFinishListener
  {
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _character;

    private HitPointsComponent _hitPointsComponent;

    private void Awake()
    {
      _hitPointsComponent = _character.GetComponent<HitPointsComponent>();
    }

    public void OnStart()
    {
      _hitPointsComponent.OnHpEmpty += OnEmptyHP;
    }

    public void OnFinish()
    {
      _hitPointsComponent.OnHpEmpty -= OnEmptyHP;
    }

    private void OnEmptyHP(GameObject obj)
    {
      _gameManager.FinishGame();
    }
  }
}