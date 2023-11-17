using UnityEngine;

namespace ShootEmUp
{
  public class BulletsFactory : Factory<Bullet>
  {
    [SerializeField] private Transform _worldTransform;
    
    public Bullet GetInitializedInstance(BulletArgs args)
    {
      if (TryGetInstance(out Bullet bullet))
      {

        bullet.SetPosition(args.Position);
        bullet.transform.SetParent(_worldTransform);
        bullet.SetColor(args.Color);
        bullet.SetPhysicsLayer(args.PhysicsLayer);
        bullet.SetVelocity(args.Velocity);

        bullet.Damage = args.Damage;
        bullet.IsPlayer = args.IsPlayer;
      }
      
      return bullet;
    }
  }
}