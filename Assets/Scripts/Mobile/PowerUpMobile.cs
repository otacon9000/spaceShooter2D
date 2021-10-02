using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMobile : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUpID = 0;
    [SerializeField]
    private AudioClip _powerUpClip;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    [Range(0, 1)]
    private float _spawnProb;


    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -8.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMobile player = other.transform.GetComponent<PlayerMobile>();

            AudioSource.PlayClipAtPoint(_powerUpClip, transform.position);

            if (player != null)
            {
                switch (_powerUpID)
                {

                    case 2:
                        player.ActivateShield();
                        break;

                    case 4:
                        player.RestoreHealth();
                        break;

                    case 6:
                        player.RemoveAmmo();
                        break;
                    default:
                        Debug.LogWarning("power up ID not found");
                        break;
                }
            }
            Destroy(this.gameObject);
        }
        if (other.gameObject.CompareTag("EnemyLaser"))
        {
            _speed = 0;
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject, 0.5f);

        }
    }
    public float GetSpawnProbability()
    {
        return _spawnProb;
    }
}
