using GamePlay.Level;
using Ifrastructure.Events;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class LevelBackground : MonoBehaviour,
    IGamePauseListener, 
    IGameResumeListener,
    IGameFixedUpdateListener,
    IGameStartListener,
    IGameFinishListener
  {
    [SerializeField] private Params _params;
    
    private float _startPositionY;
    private float _endPositionY;
    private float _movingSpeedY;
    private float _positionX;
    private float _positionZ;
    private Transform _myTransform;

    private bool _canMoveBackground = false;
    
    private void Awake()
    {
      _startPositionY = _params.StartPositionY;
      _endPositionY = _params.EndPositionY;
      _movingSpeedY = _params.MovingSpeedY;
      _myTransform = transform;

      var position = _myTransform.position;
      _positionX = position.x;
      _positionZ = position.z;
    }

    public void OnStart()
    {
      _canMoveBackground = true;
    }

    public void OnPause()
    {
      _canMoveBackground = false;
    }

    public void OnResume()
    {
      _canMoveBackground = true;
    }

    public void OnFinish()
    {
      _canMoveBackground = false;
    }

    public void OnFixedUpdate(float fixedDeltaTime)
    {
      if(!_canMoveBackground)
        return;
      
      if (_myTransform.position.y <= _endPositionY)
      {
        _myTransform.position = new Vector3(
          _positionX,
          _startPositionY,
          _positionZ
        );
      }

      _myTransform.position -= new Vector3(
        _positionX,
        _movingSpeedY * fixedDeltaTime,
        _positionZ
      );
    }
  }
}