using System.Collections.Generic;
using Ifrastructure.Events;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class BulletSystem : MonoBehaviour,
    IGameFixedUpdateListener
  {
    [SerializeField] private LevelBounds _levelBounds;
    [SerializeField] private BulletsFactory _bulletsFactory;

    private readonly List<Bullet> _cache = new();

    public void OnFixedUpdate(float fixedDeltaTime)
    {
      _cache.Clear();
      _cache.AddRange(_bulletsFactory.ActiveObjects);

      for (int i = 0, count = _cache.Count; i < count; i++)
      {
        var bullet = _cache[i];
        
        if (!_levelBounds.InBounds(bullet.transform.position))
        {
          RemoveBullet(bullet);
        }
      }
    }

    public void FlyBulletByArgs(BulletArgs args)
    {
      if (_bulletsFactory.TryGetInitializedInstance(args, out Bullet bullet))
      {
        bullet.OnCollisionEntered += OnBulletCollision;
      }
    }

    private void OnBulletCollision(Bullet bullet, Collision2D collision)
    {
      BulletUtils.DealDamage(bullet, collision.gameObject);
      RemoveBullet(bullet);
    }

    private void RemoveBullet(Bullet bullet)
    {
      if (_bulletsFactory.ReleaseInstance(bullet))
      {
        bullet.OnCollisionEntered -= OnBulletCollision;
      }
    }
  }
}