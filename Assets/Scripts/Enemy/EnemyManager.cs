using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class EnemyManager : MonoBehaviour
  {
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private BulletSystem _bulletSystem;

    private readonly HashSet<GameObject> _activeEnemies = new();

    private IEnumerator Start()
    {
      while (true)
      {
        yield return new WaitForSeconds(1);
        var enemy = _enemyFactory.GetInitializedEnemy();
        
        if (enemy != null)
        {
          if (_activeEnemies.Add(enemy))
          {
            enemy.GetComponent<HitPointsComponent>().OnHpEmpty += OnDestroyed;
            enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
          }
        }
      }
    }

    private void OnDestroyed(GameObject enemy)
    {
      if (_activeEnemies.Remove(enemy))
      {
        enemy.GetComponent<HitPointsComponent>().OnHpEmpty -= OnDestroyed;
        enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;

        _enemyFactory.ReleaseEnemy(enemy);
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