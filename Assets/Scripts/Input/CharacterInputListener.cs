using UnityEngine;

namespace ShootEmUp
{
  public sealed class CharacterInputListener : MonoBehaviour
  {
    [SerializeField] private GameObject _character;
    [SerializeField] private AttackComponent _characterAttackComponent;

    private float _horizontalDirection;

    private void Update()
    {
      if (Input.GetKeyDown(KeyCode.Space)) 
        _characterAttackComponent.Fire = true;

      if (Input.GetKey(KeyCode.LeftArrow))
        _horizontalDirection = -1;
      else if (Input.GetKey(KeyCode.RightArrow))
        _horizontalDirection = 1;
      else
        _horizontalDirection = 0;
    }

    private void FixedUpdate()
    {
      _character.GetComponent<MoveComponent>()
        .MoveByRigidbodyVelocity(new Vector2(_horizontalDirection, 0) * Time.fixedDeltaTime);
    }
  }
}