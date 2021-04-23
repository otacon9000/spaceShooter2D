using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    [System.Serializable]
    public class Waves
    {
        public int indexOfWave;
        public int maxEnemies = 10;
        public GameObject[] enemies;        
        public GameObject specialEnemy;

    }
    [SerializeField]
    private Waves[] _waves;

    [SerializeField]
    private GameObject[] _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _PowerUpPrefab;
    [SerializeField]
    private WaitForSeconds _waitEnemy = new WaitForSeconds(5.0f);

    private int _currentWave;
    private int _enemiesCounter;
    [SerializeField]
    private int _maxEnemies = 10;

    private bool _stopSpawn = false;

    void Start()
    {
        _currentWave = 1;
        _enemiesCounter = 0;
            
    }
   

   

    public void StartSpawning()
    {
        StartCoroutine(PowerUpSpawnRoutine());
        //StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(EnemyWavesRoutine());
    }


    IEnumerator EnemyWavesRoutine()
    {
        yield return new WaitForSeconds(2.0f);

        while (_stopSpawn == false)
        {
            float randomX = Random.Range(-8.0f, 8.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7f, 0);
            Vector3 spawnPos2 = new Vector3(-13f, 6f, 0f);

            switch(_currentWave)
            {
                case 1:
                    if (_enemiesCounter < _waves[_currentWave - 1].maxEnemies)
                    {
                        int randomEnemy = Random.Range(0, _waves[_currentWave - 1].enemies.Length);
                        if (Random.value <= _waves[_currentWave - 1].enemies[randomEnemy].GetComponent<Enemy>().GetSpawnProbability())
                        {
                            Instantiate(_waves[_currentWave - 1].enemies[randomEnemy], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                            _enemiesCounter++;
                            yield return _waitEnemy;
                        }
                        if(Random.value <= 0.1 && _waves[_currentWave - 1].specialEnemy != null)
                        {
                            Instantiate(_waves[_currentWave - 1].specialEnemy, spawnPos2, Quaternion.identity);
                            _enemiesCounter++;
                            yield return _waitEnemy;
                        }

                    }
                    else
                    {
                        Debug.Log("first wave ended");
                        _currentWave++;
                        _enemiesCounter = 0;
                        yield return _waitEnemy;
                    }
                    break;
                case 2:
                    if (_enemiesCounter < _waves[_currentWave - 1].maxEnemies)
                    {
                        int randomEnemy = Random.Range(0, _waves[_currentWave - 1].enemies.Length);
                        if (Random.value <= _waves[_currentWave - 1].enemies[randomEnemy].GetComponent<Enemy>().GetSpawnProbability())
                        {
                            Instantiate(_waves[_currentWave - 1].enemies[randomEnemy], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                            _enemiesCounter++;
                            yield return _waitEnemy;
                        }
                        if (Random.value <= 0.1 && _waves[_currentWave - 1].specialEnemy != null)
                        {
                            Instantiate(_waves[_currentWave - 1].specialEnemy, spawnPos2, Quaternion.identity);
                            _enemiesCounter++;
                            yield return _waitEnemy;
                        }

                    }
                    else
                    {
                        Debug.Log("second wave ended");
                        _currentWave++;
                        _enemiesCounter = 0;
                        yield return _waitEnemy;
                    }
                    break;
                default:
                    Debug.Log("Def");
                    yield return _waitEnemy;
                    break;

            }
        }
    }


    IEnumerator EnemySpawnRoutine()
    {
        yield return new WaitForSeconds(2.0f);
        
        while(_stopSpawn == false)
        {
            
            float randomX = Random.Range(-8.0f, 8.0f);
            Vector3 spawnPosition = new Vector3(randomX, 7f, 0);
            Vector3 spawnPos2 = new Vector3(-13f, 6f, 0f);
            if (_enemiesCounter < _maxEnemies)
            {
                if (Random.value <= 0.7)
                {
                    Instantiate(_enemyPrefab[0], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                    _enemiesCounter++;
                    yield return _waitEnemy;
                }

                if (Random.value <= 0.5)
                {
                    Instantiate(_enemyPrefab[1], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                    _enemiesCounter++;
                    yield return _waitEnemy;
                }

                if (Random.value <= 0.1)
                {
                    Instantiate(_enemyPrefab[2], spawnPos2, Quaternion.identity, _enemyContainer.transform);
                    _enemiesCounter++;
                    yield return _waitEnemy;
                }
                if (Random.value <= 0.4)
                {
                    Instantiate(_enemyPrefab[3], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                    _enemiesCounter++;
                    yield return _waitEnemy;
                }
            }
            else
            {
                Debug.Log("end of wave");
                yield return null;
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
