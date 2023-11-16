using UnityEngine;

namespace ShootEmUp
{
  public sealed class EnemyFactory : MonoBehaviour
  {
    [Header("Spawn")]
    [SerializeField] private EnemyPositions _enemyPositions;
    [SerializeField] private GameObject _character;
    [SerializeField] private Transform _worldTransform;

    [Header("Pool")] 
    [SerializeField] private Transform _container;
    [SerializeField] private GameObject _prefab;

    private ObjectPool<GameObject> _enemiesPool;

    private void Awake() => 
      _enemiesPool = new ObjectPool<GameObject>(_prefab, container: _container);

    public GameObject GetInitializedEnemy()
    {
      var enemy = _enemiesPool.GetInstance();
      
      enemy.transform.SetParent(_worldTransform);

      var spawnPosition = _enemyPositions.RandomSpawnPosition();
      enemy.transform.position = spawnPosition.position;

      var attackPosition = _enemyPositions.RandomAttackPosition();
      enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

      enemy.GetComponent<EnemyAttackAgent>().SetTarget(_character);
      return enemy;
    }

    public void ReleaseEnemy(GameObject enemy) => 
      _enemiesPool.Release(enemy, _container);
  }
}