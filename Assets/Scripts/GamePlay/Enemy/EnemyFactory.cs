using Ifrastructure;
using Ifrastructure.Events;
using UnityEngine;

namespace ShootEmUp
{
  public sealed class EnemyFactory : Factory<GameObject>
  {
    [Header("Spawn")] 
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private EnemyPositions _enemyPositions;
    [SerializeField] private GameObject _character;
    [SerializeField] private Transform _worldTransform;

    public bool TryGetInitializedInstance(out GameObject enemy)
    {
      if (TryGetInstance(out enemy))
      {
        enemy.transform.SetParent(_worldTransform);

        var spawnPosition = _enemyPositions.RandomSpawnPosition();
        enemy.transform.position = spawnPosition.position;

        var attackPosition = _enemyPositions.RandomAttackPosition();
        
        enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);
        enemy.GetComponent<EnemyAttackAgent>().SetTarget(_character);
        
        EnableEnemy(enemy);
      }

      return enemy;
    }

    private void EnableEnemy(GameObject enemy)
    {
      var gameListeners = enemy.GetComponents<IGameListener>();
      _gameManager.AddListeners(gameListeners);
    }

    private void DisableEnemy(GameObject enemy)
    {
      var gameListeners = enemy.GetComponents<IGameListener>();
      _gameManager.RemoveListeners(gameListeners);
    }

    public override bool ReleaseInstance(GameObject instance)
    {
      if (base.ReleaseInstance(instance))
      {
        DisableEnemy(instance);
        return true;
      }

      return false;
    }
  }
}