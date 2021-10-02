using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerM : MonoBehaviour
{
    private static SpawnManagerM _instance;
    public static SpawnManagerM Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("SpawnManager is NULL");
            }
            return _instance;
        }
    }

    [Header("Enemies")]
    [SerializeField]
    private GameObject[] _enemiesPrefab;
    public int enemiesInScene = 0;

    [Header("Power Up")]
    [SerializeField]
    private GameObject[] _powerUpsPrefab;
    private bool _stopSpawn;

    private int[] xPosSpawn = { -2, 0, 2 };

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        StartCoroutine(EnemiesSpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }


    IEnumerator EnemiesSpawnRoutine()
    {
        while (_stopSpawn == false)
        {
            yield return new WaitForSeconds(Random.Range(2, 6));
            int randomX = Random.Range(0, 3);
            
            Vector3 spawnPosition = new Vector3(xPosSpawn[randomX], 9, 0);
            int randomEnemy = Random.Range(0, _enemiesPrefab.Length);
            if (enemiesInScene <= 10)
            {
                Instantiate(_enemiesPrefab[randomEnemy], spawnPosition, Quaternion.identity);
                enemiesInScene++;
            }

        }

    }


    IEnumerator PowerUpSpawnRoutine()
    {
        while (_stopSpawn == false)
        {
            yield return new WaitForSeconds(Random.Range(2, 6));
            int randomX = Random.Range(0, 3);
            Vector3 spawnPosition = new Vector3(xPosSpawn[randomX], 9, 0);
            int randomPowerUp = Random.Range(0, _powerUpsPrefab.Length);

            if (Random.value <= _powerUpsPrefab[randomPowerUp].GetComponent<PowerUpMobile>().GetSpawnProbability())
            {
                Instantiate(_powerUpsPrefab[randomPowerUp], spawnPosition, Quaternion.identity);
            }
        }

    }
    
    public void OnEndGame()
    {
        _stopSpawn = true;
        StopAllCoroutines();
    }

    public void EnemyDestroyed()
    {
        enemiesInScene--;
    }

}
