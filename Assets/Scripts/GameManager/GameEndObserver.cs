using UnityEngine;

namespace ShootEmUp
{
  public class GameEndObserver : MonoBehaviour
  {
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private GameObject _character;

    private HitPointsComponent _hitPointsComponent;

    private void Awake() => 
      _hitPointsComponent = _character.GetComponent<HitPointsComponent>();

    private void OnEnable() => 
      _hitPointsComponent.OnHpEmpty += OnEmptyHP;

    private void OnDisable() => 
      _hitPointsComponent.OnHpEmpty -= OnEmptyHP;

    private void OnEmptyHP(GameObject obj) => 
      _gameManager.FinishGame();
  }
}