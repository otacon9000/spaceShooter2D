using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawn = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    //spawn a game objects every 5 seconds 
    //create a coroutine of IEnumerator -- Yield return 
    //while loop 
    IEnumerator SpawnRoutine()
    {
        while(_stopSpawn == false)
        {
            float randomX = Random.Range(-8, 8);
            Vector3 spawnPosition = new Vector3(randomX, 7f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5);
        }

    }

    public void OnPlayerDeath()
    {
        _stopSpawn = true;
    }
}
