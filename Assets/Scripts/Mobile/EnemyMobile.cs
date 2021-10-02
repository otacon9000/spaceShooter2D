using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMobile : MonoBehaviour
{
    private Rigidbody2D _rb;
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
    private bool _shildIsActive = false;
    [SerializeField]
    private GameObject _shields;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;


    private PlayerMobile _player;
    private int[] xPosSpawn = { -2, 0, 2 };

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerMobile>();
        _rb = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();


        if (_player == null)
        {
            Debug.LogError("The player is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL.");
        }
        else
        {
            _audioSource.clip = _explosionClip;
        }

        if (_rb == null)
        {
            Debug.Log("Rigidbody2D is Null.");
        }
    }

    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            FireLaser();
        }
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (_player != null)
            {
                _player.Damage();
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 1f);
        }
        if (other.CompareTag("Laser"))
        {
            if (_shildIsActive == true)
            {
                Destroy(other.gameObject);
                DeactivateShields();
            }
            else
            {
                Destroy(other.gameObject);

                if (_player != null)
                {
                    _player.AddScore(_points);
                }
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _speed = 0;
                _audioSource.Play();
                SpawnManagerM.Instance.EnemyDestroyed();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 1f);


            }
        }

    }

    void DeactivateShields()
    {
        if (_shildIsActive)
        {
            _shildIsActive = false;
            _shields.SetActive(_shildIsActive);
            
        }
    }
}
