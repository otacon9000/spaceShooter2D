using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
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
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return _waitEnemy;
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
                randomPowerUp = Random.Range(0, 6);
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
