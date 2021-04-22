using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //[System.Serializable]
    //public class Waves
    //{
    //    public int indexOfWave;
    //    public GameObject[] enemies;
    //    public int maxEnemies = 10; 
      
    //}
    //[SerializeField]
    //private Waves[] _waves;
    [SerializeField]
    private GameObject[] _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _PowerUpPrefab;
    [SerializeField]
    private WaitForSeconds _waitEnemy = new WaitForSeconds(5.0f);



    private bool _stopSpawn = false;

    private PowerUp _currentPowerUp;

    public void StartSpawning()
    {
        StartCoroutine(PowerUpSpawnRoutine());
        StartCoroutine(EnemySpawnRoutine());
    }



    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        
        while(_stopSpawn == false)
        {
            
            float randomX = Random.Range(-8.0f, 8.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7f, 0);
            Vector3 spawnPos2 = new Vector3(-13f, 6f, 0f);

            if (Random.value <= 0.7) 
            {
                Instantiate(_enemyPrefab[0], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                yield return _waitEnemy;
            }

            if (Random.value <= 0.5)
            {
                Instantiate(_enemyPrefab[1], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                yield return _waitEnemy;
            }

            if (Random.value <= 0.1) 
            {
                Instantiate(_enemyPrefab[2], spawnPos2, Quaternion.identity, _enemyContainer.transform);
                yield return _waitEnemy;
            }
            if (Random.value <= 0.4) 
            {
                Instantiate(_enemyPrefab[3], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                yield return _waitEnemy;
            }


        }
    }

    
    IEnumerator PowerUpSpawnRoutine()
    {
        while(_stopSpawn == false)
        {
            yield return new WaitForSeconds(Random.Range(2,6));
            float randomX = Random.Range(-8.0f, 8.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7f, 0);
            int randomPowerUp = Random.Range(0, _PowerUpPrefab.Length);


            if (Random.value <= _PowerUpPrefab[randomPowerUp].GetComponent<PowerUp>().GetSpawnProbability()) 
            {
                Instantiate(_PowerUpPrefab[randomPowerUp], spawnPosition, Quaternion.identity);
            }

        }
        
    }

    public void OnPlayerDeath()
    {
        _stopSpawn = true;
        StopAllCoroutines();
    }
}
