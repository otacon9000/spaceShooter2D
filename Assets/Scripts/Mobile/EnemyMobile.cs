using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMobile : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private int _points = 10;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 3.0f;
    private float _canFire = 0.5f;
    [SerializeField]
    private GameObject _explosionPrefab;

    private int[] xPosSpawn = { -2, 0, 2 };


    void Update()
    {
        CalculateMovement();
    }

    public virtual void  CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7f)
        {
            int randomX = Random.Range(0, 3);
            transform.position = new Vector3(xPosSpawn[randomX], 9f, 0);
        }
    }

    public virtual void FireLaser()
    {
        _fireRate = Random.Range(2f, 4f);
        _canFire = Time.time + _fireRate;
        GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
        Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

        foreach (Laser element in lasers)
        {
            element.AssingEnemyLaser();
        }
    }

    private void OnDestroy()
    {
        SpawnManagerM.Instance.enemiesInScene--;
    }

}
