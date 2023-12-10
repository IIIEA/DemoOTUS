using System.Collections.Generic;
using Ifrastructure.Events;
using ShootEmUp;
using UnityEngine;

namespace Ifrastructure.Providers
{
  public class InputsInstaller : MonoBehaviour, 
    IGameListenersProvider
  {
    [SerializeField] private CharacterInputListener _inputListener;
  
    public IEnumerable<IGameListener> ProvideListeners()
    {
      yield return _inputListener;
    }
  }
}