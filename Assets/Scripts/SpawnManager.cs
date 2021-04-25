using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    [System.Serializable]
    public class Waves
    {
        public int maxEnemies = 10;
        public GameObject[] enemies;        
        public GameObject specialEnemy;

    }

    [Header("Enemy Waves")]
    [SerializeField]
    private Waves _firstWave;
    [SerializeField]
    private Waves _secondWave;
    [SerializeField]
    private Waves _bossWave;
    private int _currentWave;
    private int _enemiesCounter;
    [SerializeField]
    private GameObject _enemyContainer;

    [Header("Power-Ups")]
    [SerializeField]
    private GameObject[] _PowerUpPrefab;
    [SerializeField]
    private WaitForSeconds _waitEnemy = new WaitForSeconds(5.0f);

    private bool _stopSpawn = false;
    private bool _bossIsSpawned = false;

    void Start()
    {
        _currentWave = 1;
        _enemiesCounter = 0;
        _bossIsSpawned = false;
    }
   
    public void StartSpawning()
    {
        StartCoroutine(PowerUpSpawnRoutine());
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
                    if (_enemiesCounter < _firstWave.maxEnemies)
                    {
                        int randomEnemy = Random.Range(0, _firstWave.enemies.Length);
                        if (Random.value <= _firstWave.enemies[randomEnemy].GetComponent<Enemy>().GetSpawnProbability())
                        {
                            Instantiate(_firstWave.enemies[randomEnemy], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                            _enemiesCounter++;
                            yield return _waitEnemy;
                        }
                        if(Random.value <= 0.1 && _firstWave.specialEnemy != null)
                        {
                            Instantiate(_firstWave.specialEnemy, spawnPos2, Quaternion.identity);
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
                    if (_enemiesCounter < _secondWave.maxEnemies)
                    {
                        int randomEnemy = Random.Range(0, _secondWave.enemies.Length);
                        if (Random.value <= _secondWave.enemies[randomEnemy].GetComponent<Enemy>().GetSpawnProbability())
                        {
                            Instantiate(_secondWave.enemies[randomEnemy], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                            _enemiesCounter++;
                            yield return _waitEnemy;
                        }
                        if (Random.value <= 0.1 && _secondWave.specialEnemy != null)
                        {
                            Instantiate(_secondWave.specialEnemy, spawnPos2, Quaternion.identity);
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
                case 3:
                    if (_enemiesCounter < _bossWave.maxEnemies && _bossWave != null)
                    {
                        int randomEnemy = Random.Range(0, _bossWave.enemies.Length);
                        if (Random.value <= _bossWave.enemies[randomEnemy].GetComponent<Enemy>().GetSpawnProbability())
                        {
                            Instantiate(_bossWave.enemies[randomEnemy], spawnPosition, Quaternion.identity, _enemyContainer.transform);
                            _enemiesCounter++;
                            yield return _waitEnemy;
                        }
                    }
                    else
                    {
                        Debug.Log("Boss wave ended");
                        if (_bossIsSpawned == false && _bossWave.specialEnemy != null)
                        {
                            _bossIsSpawned = true;
                            Instantiate(_bossWave.specialEnemy, new Vector3(0, 10, 0), Quaternion.identity);
                            _currentWave++;
                            yield return _waitEnemy;
                        }
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

    public void OnEndGame()
    {
        _stopSpawn = true;
        StopAllCoroutines();
    }
}
