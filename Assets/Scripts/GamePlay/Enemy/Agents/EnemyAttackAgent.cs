using Ifrastructure.Events;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class EnemyAttackAgent : MonoBehaviour,
    IGameFixedUpdateListener,
    IGamePauseListener,
    IGameResumeListener,
    IGameFinishListener
  {
    [SerializeField] private WeaponComponent _weaponComponent;
    [SerializeField] private EnemyMoveAgent _moveAgent;
    [SerializeField] private float _countdown;

    private GameObject _target;
    private float _currentTime;
    private bool _canAttack = true;

    public event FireHandler OnFire;
    public delegate void FireHandler(GameObject enemy, Vector2 position, Vector2 direction);

    public void SetTarget(GameObject target)
    {
      _target = target;
    }

    public void Reset()
    {
      _currentTime = _countdown;
    }

    private void Fire()
    {
      var startPosition = _weaponComponent.Position;
      var vector = (Vector2)_target.transform.position - startPosition;
      var direction = vector.normalized;
      OnFire?.Invoke(gameObject, startPosition, direction);
    }

    public void OnFixedUpdate(float fixedDeltaTime)
    {
      if(!_canAttack)
        return;
      
      if (!_moveAgent.IsReached)
      {
        return;
      }

      if (!_target.GetComponent<HitPointsComponent>().IsHitPointsExists())
      {
        return;
      }

      _currentTime -= fixedDeltaTime;
      
      if (_currentTime <= 0)
      {
        Fire();
        _currentTime += _countdown;
      }
    }

    public void OnPause()
    {
      _canAttack = false;
    }

    public void OnResume()
    {
      _canAttack = true;
    }

    public void OnFinish()
    {
      _canAttack = false;
    }
  }
}