using UnityEngine;

namespace ShootEmUp
{
  public sealed class CharacterController : MonoBehaviour
  {
    [SerializeField] private GameObject _character;
    [SerializeField] private BulletSystem _bulletSystem;
    [SerializeField] private BulletConfig _bulletConfig;

    private bool _fireRequired;
    
    public bool Fire
    {
      set => _fireRequired = value;
    }

    private void FixedUpdate()
    {
      if (_fireRequired)
      {
        OnFlyBullet();
        _fireRequired = false;
      }
    }
    
    private void OnFlyBullet()
    {
      var weapon = _character.GetComponent<WeaponComponent>();
      _bulletSystem.FlyBulletByArgs(new BulletArgs
      {
        IsPlayer = true,
        PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
        Color = _bulletConfig.Color,
        Damage = _bulletConfig.Damage,
        Position = weapon.Position,
        Velocity = weapon.Rotation * Vector3.up * _bulletConfig.Speed
      });
    }
  }
}