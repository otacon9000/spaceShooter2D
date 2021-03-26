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
    private GameObject _powerUpPrefab;
    [SerializeField]
    private GameObject _powerUpContainer;
    [SerializeField]
    private WaitForSeconds _waitEnemy = new WaitForSeconds(5.0f);
    private WaitForSeconds _waitPowerUp = new WaitForSeconds(10.0f);


    private bool _stopSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(PowerUpSpawnRoutine());
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
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
            yield return _waitPowerUp;
            float randomX = Random.Range(-8.0f, 8.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7f, 0);
            GameObject newPowerUp = Instantiate(_powerUpPrefab, spawnPosition, Quaternion.identity);
            newPowerUp.transform.parent = _powerUpContainer.transform;
            
        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawn = true;
        StopAllCoroutines();
    }
}
