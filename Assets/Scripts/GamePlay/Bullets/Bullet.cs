using System;
using Ifrastructure.Events;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class Bullet : MonoBehaviour,
    IGamePauseListener,
    IGameResumeListener,
    IGameFinishListener
  {
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private bool _isPlayer;
    private int _damage;

    public bool IsPlayer => _isPlayer;
    public int Damage => _damage;

    public event Action<Bullet, Collision2D> OnCollisionEntered;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
      OnCollisionEntered?.Invoke(this, collision);
    }

    public void SetPlayer(bool isPlayer)
    {
      _isPlayer = isPlayer;
    }

    public void SetDamage(int damage)
    {
      _damage = damage;
    }

    public void SetVelocity(Vector2 velocity)
    {
      _rigidbody2D.velocity = velocity;
    }

    public void SetPhysicsLayer(int physicsLayer)
    {
      gameObject.layer = physicsLayer;
    }

    public void SetPosition(Vector3 position)
    {
      transform.position = position;
    }

    public void SetColor(Color color)
    {
      _spriteRenderer.color = color;
    }

    public void OnPause()
    {
      _rigidbody2D.simulated = false;
    }

    public void OnResume()
    {
      _rigidbody2D.simulated = true;
    }

    public void OnFinish()
    {
      _rigidbody2D.simulated = false;
    }
  }
}