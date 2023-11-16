using System;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class LevelBackground : MonoBehaviour
  {
    [SerializeField] private Params _params;
    
    private float startPositionY;
    private float endPositionY;
    private float movingSpeedY;
    private float positionX;
    private float positionZ;
    private Transform myTransform;
    
    private void Awake()
    {
      startPositionY = _params.StartPositionY;
      endPositionY = _params.EndPositionY;
      movingSpeedY = _params.MovingSpeedY;
      myTransform = transform;

      var position = myTransform.position;
      positionX = position.x;
      positionZ = position.z;
    }

    private void FixedUpdate()
    {
      if (myTransform.position.y <= endPositionY)
      {
        myTransform.position = new Vector3(
          positionX,
          startPositionY,
          positionZ
        );
      }

      myTransform.position -= new Vector3(
        positionX,
        movingSpeedY * Time.fixedDeltaTime,
        positionZ
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