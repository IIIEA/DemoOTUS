using System;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class Bullet : MonoBehaviour
  {
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _isPlayer;
    private int _damage;

    public bool IsPlayer
    {
      get => _isPlayer;
      set => _isPlayer = value;
    }
    
    public int Damage
    {
      get => _damage;
      set => _damage = value;
    }

    public event Action<Bullet, Collision2D> OnCollisionEntered;

    private void OnCollisionEnter2D(Collision2D collision)
    {
      print($"Collide with : {collision.gameObject.name}");
      OnCollisionEntered?.Invoke(this, collision);
    }

    public void SetVelocity(Vector2 velocity) => 
      _rigidbody2D.velocity = velocity;

    public void SetPhysicsLayer(int physicsLayer) => 
      gameObject.layer = physicsLayer;

    public void SetPosition(Vector3 position) => 
      transform.position = position;

    public void SetColor(Color color) => 
      _spriteRenderer.color = color;
  }
}