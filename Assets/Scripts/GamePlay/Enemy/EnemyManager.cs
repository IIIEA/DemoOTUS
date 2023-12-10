using UnityEngine;

namespace ShootEmUp
{
  public sealed class EnemyManager : MonoBehaviour
  {
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private BulletSystem _bulletSystem;

    public void SpawnEnemy()
    {
      if (_enemyFactory.TryGetInitializedInstance(out GameObject enemy))
      {
        enemy.GetComponent<HitPointsComponent>().OnHpEmpty += OnDestroyed;
        enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
      }
    }

    private void OnDestroyed(GameObject enemy)
    {
      if (_enemyFactory.ReleaseInstance(enemy))
      {
        enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= OnDestroyed;
        enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;
      }
    }

    private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
    {
      _bulletSystem.FlyBulletByArgs(new BulletArgs
      {
        IsPlayer = false,
        PhysicsLayer = (int)PhysicsLayer.ENEMY_BULLET,
        Color = Color.red,
        Damage = 1,
        Position = position,
        Velocity = direction * 2.0f
      });
    }
  }
}