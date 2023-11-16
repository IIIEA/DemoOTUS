using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class BulletSystem : MonoBehaviour
  {
    [SerializeField] private Transform _worldTransform;
    [SerializeField] private LevelBounds _levelBounds;
    [SerializeField] private BulletsFactory _bulletsFactory;
    
    private readonly HashSet<Bullet> _activeBullets = new();
    private readonly List<Bullet> _cache = new();

    private void FixedUpdate()
    {
      _cache.Clear();
      _cache.AddRange(_activeBullets);

      for (int i = 0, count = _cache.Count; i < count; i++)
      {
        var bullet = _cache[i];
        if (_levelBounds.InBounds(bullet.transform.position) == false)
        {
          RemoveBullet(bullet);
        }
      }
    }

    public void FlyBulletByArgs(BulletArgs args)
    {
      var bullet = _bulletsFactory.GetInitializedBullet(args, _worldTransform);
      bullet.OnCollisionEntered += OnBulletCollision;
      
      _activeBullets.Add(bullet);
    }

    private void OnBulletCollision(Bullet bullet, Collision2D collision)
    {
      BulletUtils.DealDamage(bullet, collision.gameObject);
      RemoveBullet(bullet);
    }

    private void RemoveBullet(Bullet bullet)
    {
      if (_activeBullets.Remove(bullet))
      {
        bullet.OnCollisionEntered -= OnBulletCollision;
        _bulletsFactory.ReleaseBullet(bullet);
      }
    }
  }
}