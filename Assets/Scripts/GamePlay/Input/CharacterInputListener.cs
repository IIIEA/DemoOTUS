using Ifrastructure.Events;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class CharacterInputListener : MonoBehaviour,
    IGameUpdateListener,
    IGameFixedUpdateListener,
    IGamePauseListener,
    IGameResumeListener,
    IGameStartListener,
    IGameFinishListener
  {
    [SerializeField] private GameObject _character;
    [SerializeField] private AttackComponent _characterAttackComponent;

    private float _horizontalDirection;
    private bool _isInputsEnable;

    public void OnUpdate(float deltaTime)
    {
      if(!_isInputsEnable)
        return;
      
      if (Input.GetKeyDown(KeyCode.Space))
      {
        _characterAttackComponent.Fire = true;
      }

      if (Input.GetKey(KeyCode.LeftArrow))
      {
        _horizontalDirection = -1;
      }
      else if (Input.GetKey(KeyCode.RightArrow))
      {
        _horizontalDirection = 1;
      }
      else
      {
        _horizontalDirection = 0;
      }
    }

    public void OnFixedUpdate(float fixedDeltaTime)
    {
      if(!_isInputsEnable)
        return;
      
      _character.GetComponent<MoveComponent>()
        .MoveByRigidbodyVelocity(new Vector2(_horizontalDirection, 0) * fixedDeltaTime);
    }

    public void OnPause()
    {
      _isInputsEnable = false;
    }

    public void OnResume()
    {
      _isInputsEnable = true;
    }

    public void OnStart()
    {
      _isInputsEnable = true;
    }

    public void OnFinish()
    {
      _isInputsEnable = false;
    }
  }
}