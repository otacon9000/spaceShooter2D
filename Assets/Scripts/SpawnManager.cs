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
    private GameObject _tripleShotPowerUpPrefab;
    [SerializeField]
    private WaitForSeconds _waitEnemy = new WaitForSeconds(5.0f);
    //private WaitForSeconds _waitPowerUp = new WaitForSeconds(10.0f);


    private bool _stopSpawn = false;
    void Start()
    {
        StartCoroutine(PowerUpSpawnRoutine());
        StartCoroutine(EnemySpawnRoutine());
    }

    IEnumerator EnemySpawnRoutine()
    {
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
            Instantiate(_tripleShotPowerUpPrefab, spawnPosition, Quaternion.identity);
            
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawn = true;
        StopAllCoroutines();
    }
}
