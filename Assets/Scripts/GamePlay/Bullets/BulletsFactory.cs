using Ifrastructure;
using UnityEngine;

namespace ShootEmUp
{
  public class BulletsFactory : Factory<Bullet>
  {
    [SerializeField] private Transform _worldTransform;
    [SerializeField] private GameManager _gameManager;
    
    public bool TryGetInitializedInstance(BulletArgs args, out Bullet bullet)
    {
      if (TryGetInstance(out bullet))
      {
        bullet.SetPosition(args.Position);
        bullet.transform.SetParent(_worldTransform);
        bullet.SetColor(args.Color);
        bullet.SetPhysicsLayer(args.PhysicsLayer);
        bullet.SetVelocity(args.Velocity);

        bullet.SetDamage(args.Damage);
        bullet.SetPlayer(args.IsPlayer);
        
        _gameManager.AddListener(bullet);
      }
      
      return bullet;
    }

    public override bool ReleaseInstance(Bullet instance)
    {
      if (base.ReleaseInstance(instance))
      {
        _gameManager.RemoveListener(instance);
        return true;
      }

      return false;
    }
  }
}