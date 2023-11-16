using UnityEngine;

namespace ShootEmUp
{
  public class BulletsFactory : MonoBehaviour
  {
    [SerializeField] private int _initialCount = 50;
    [SerializeField] private Transform _container;
    [SerializeField] private Bullet _prefab;

    private ObjectPool<Bullet> _bulletPool;
    
    private void Awake() => 
      _bulletPool = new ObjectPool<Bullet>(_prefab, _initialCount, _container);

    public Bullet GetInitializedBullet(BulletArgs args, Transform container)
    {
      var bullet = _bulletPool.GetInstance();

      bullet.SetParent(container);
      bullet.SetPosition(args.Position);
      bullet.SetColor(args.Color);
      bullet.SetPhysicsLayer(args.PhysicsLayer);
      bullet.SetVelocity(args.Velocity);
      
      bullet.Damage = args.Damage;
      bullet.IsPlayer = args.IsPlayer;

      return bullet;
    }

    public void ReleaseBullet(Bullet bullet) => 
      _bulletPool.Release(bullet, _container);
  }
}