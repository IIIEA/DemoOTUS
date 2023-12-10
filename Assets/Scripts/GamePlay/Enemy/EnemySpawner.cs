using System.Collections;
using Ifrastructure.Events;
using ShootEmUp;
using UnityEngine;

namespace GamePlay.Enemy
{
  public class EnemySpawner : MonoBehaviour, 
    IGameStartListener, 
    IGameFinishListener, 
    IGamePauseListener,
    IGameResumeListener
  {
    private const float DELAY = 1f;

    [SerializeField] private EnemyManager _enemyManager;

    private bool _canSpawn;
    private WaitForSeconds _spawnDelay = new(DELAY);
    private Coroutine _spawnRoutine;

    public void OnStart()
    {
      _canSpawn = true;
      _spawnRoutine = StartCoroutine(EnemySpawnRoutine());
    }

    public void OnFinish()
    {
      _canSpawn = false;
      StopCoroutine(_spawnRoutine);
    }

    public void OnPause()
    {
      _canSpawn = false;
    }

    public void OnResume()
    {
      _canSpawn = true;
    }

    private IEnumerator EnemySpawnRoutine()
    {
      while (true)
      {
        yield return _spawnDelay;

        if (_canSpawn)
        {
          _enemyManager.SpawnEnemy();
        }
      }
    }
  }
}