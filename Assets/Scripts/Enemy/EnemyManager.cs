using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class EnemyManager : MonoBehaviour
  {
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private BulletSystem _bulletSystem;

    private IEnumerator Start()
    {
      while (true)
      {
        yield return new WaitForSeconds(1);
        var enemy = _enemyFactory.GetInitializedInstance();

        if (enemy != null)
        {
          enemy.GetComponent<HitPointsComponent>().OnHpEmpty += OnDestroyed;
          enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
        }
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