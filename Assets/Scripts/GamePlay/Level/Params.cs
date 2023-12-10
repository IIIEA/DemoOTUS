using System;
using UnityEngine;

namespace GamePlay.Level
{
  [Serializable]
  public sealed class Params
  {
    [field: SerializeField] public float StartPositionY { get; private set; }
    [field: SerializeField] public float EndPositionY { get; private set; }
    [field: SerializeField] public float MovingSpeedY { get; private set; }
  }
}