using System;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class LevelBackground : MonoBehaviour
  {
    [SerializeField] private Params _params;
    
    private float _startPositionY;
    private float _endPositionY;
    private float _movingSpeedY;
    private float _positionX;
    private float _positionZ;
    private Transform _myTransform;
    
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

    private void FixedUpdate()
    {
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
        _movingSpeedY * Time.fixedDeltaTime,
        _positionZ
      );
    }

    [Serializable]
    public sealed class Params
    {
      [field: SerializeField] public float StartPositionY { get; private set; }
      [field: SerializeField] public float EndPositionY { get; private set; }
      [field: SerializeField] public float MovingSpeedY { get; private set; }
    }
  }
}