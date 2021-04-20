using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _PowerUpPrefab;
    [SerializeField]
    private WaitForSeconds _waitEnemy = new WaitForSeconds(5.0f);


    private bool _stopSpawn = false;

    public void StartSpawning()
    {
        StartCoroutine(PowerUpSpawnRoutine());
        StartCoroutine(EnemySpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        
        while(_stopSpawn == false)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7f, 0);
            Vector3 spawnPos2 = new Vector3(-13f, 6f, 0f);
            switch (Random.Range(0, _enemyPrefab.Length)) 
            {
                case 0:
                   Instantiate(_enemyPrefab[0], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                    yield return _waitEnemy;
                    break;             
                case 1:
                   Instantiate(_enemyPrefab[1], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                    yield return _waitEnemy;
                    break;
                case 2:

                    Instantiate(_enemyPrefab[2], spawnPos2 , Quaternion.identity, _enemyContainer.transform);
                    yield return _waitEnemy;
                    break;
                case 3:
                    Instantiate(_enemyPrefab[3], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                    yield return _waitEnemy;
                    break;
            }
                
         
        }
    }

    
    IEnumerator PowerUpSpawnRoutine()
    {
        while(_stopSpawn == false)
        {
            yield return new WaitForSeconds(Random.Range(3, 8));
            float randomX = Random.Range(-8.0f, 8.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7f, 0);
            int randomPowerUp = Random.Range(0, 6);
            if (randomPowerUp == 6)
            {
                randomPowerUp = Random.Range(0, _PowerUpPrefab.Length);
            }
            Instantiate(_PowerUpPrefab[randomPowerUp], spawnPosition, Quaternion.identity);
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawn = true;
        StopAllCoroutines();
    }
}
