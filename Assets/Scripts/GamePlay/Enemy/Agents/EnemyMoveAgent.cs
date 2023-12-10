using Ifrastructure.Events;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class EnemyMoveAgent : MonoBehaviour, 
    IGameFixedUpdateListener,
    IGamePauseListener,
    IGameResumeListener,
    IGameFinishListener
  {
    [SerializeField] private MoveComponent _moveComponent;

    private Vector2 _destination;
    private bool _isReached;
    private bool _canMove = true;

    public bool IsReached => _isReached;

    public void SetDestination(Vector2 endPoint)
    {
      _destination = endPoint;
      _isReached = false;
    }

    public void OnFixedUpdate(float fixedDeltaTime)
    {
      if(!_canMove)
        return;
      
      if (_isReached)
      {
        return;
      }

      var vector = _destination - (Vector2)transform.position;

      if (vector.magnitude <= 0.25f)
      {
        _isReached = true;
        return;
      }

      var direction = vector.normalized * fixedDeltaTime;
      _moveComponent.MoveByRigidbodyVelocity(direction);
    }

    public void OnPause()
    {
      _canMove = false;
    }

    public void OnResume()
    {
      _canMove = true;
    }

    public void OnFinish()
    {
      _canMove = false;
    }
  }
}