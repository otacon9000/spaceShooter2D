using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum typeOfEnemy { normal, shields, scoreEnemy, aggressive, boss };
    public enum bossState { spawn, move, superAttack, die}
    [SerializeField]
    private typeOfEnemy _enemyType;
    [SerializeField]
    private int _enemyLives = 1;
    [SerializeField]
    private float _speed = 4.0f;
    private int _speedMultiplier = 5;
    [SerializeField]
    private int _points = 10;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 3.0f;
    private float _canFire = 0.5f;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;

    private Player _player;
    private Rigidbody2D _rb;
    [SerializeField]
    private bool _shildIsActive = false;
    [SerializeField]
    private GameObject _shields;

    [SerializeField]
    [Range(0, 1)]
    private float _spawnProb;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();      
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();

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

        if (_enemyType == typeOfEnemy.shields)   
        {
            _shildIsActive = true;


            _shields.gameObject.SetActive(true);
        }
    }

  

    void Update()
    {
        CalculateMove();
        if(Time.time > _canFire && _enemyType != typeOfEnemy.aggressive)
        {
            FireLaser();
        }

        if (_enemyType == typeOfEnemy.scoreEnemy && transform.position.x > 13.5f)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            if (_player != null)
            {
                _player.Damage();
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,1f);
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
                _enemyLives--;
                if (_enemyLives < 1)
                {

                    if (_player != null)
                    {
                        _player.AddScore(_points);
                    }
                    Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                    _speed = 0;
                    _audioSource.Play();
                    Destroy(GetComponent<Collider2D>());
                    Destroy(this.gameObject, 1f);
                }
            
            }
        }
    
    }

    void CalculateMove()
    {
        
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 3f);

        switch (_enemyType) 
        {
            case typeOfEnemy.normal:
                transform.Translate(Vector3.down * _speed * Time.deltaTime);

                if (transform.position.y < -6f)
                {
                    float randomX = Random.Range(-8, 8);
                    transform.position = new Vector3(randomX, 7f, 0);
                }
                break;

            case typeOfEnemy.shields:
                transform.Translate(Vector3.down * _speed * Time.deltaTime);

                if (transform.position.y < -6f)
                {
                    float randomX = Random.Range(-8, 8);
                    transform.position = new Vector3(randomX, 7f, 0);
                }
                break;

            case typeOfEnemy.scoreEnemy:
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
                break;

            case typeOfEnemy.aggressive:
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
               
                if (transform.position.y < -6f)
                {
                    float randomX = Random.Range(-8, 8);
                    transform.position = new Vector3(randomX, 7f, 0);
                }
                if (hit != null && (hit.CompareTag("Player") || hit.CompareTag("Player Shields")))
                {
                    Vector3 playerPosition = hit.transform.position;
                    transform.position = Vector3.Lerp(transform.position, playerPosition, _speed  * Time.deltaTime);
                }
                break;
            case typeOfEnemy.boss:
                //do boss behavior
                break;

            default:
                break;

        }

    }

    void FireLaser()
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

    void DeactivateShields()
    {
        if(_shildIsActive)
        {
            _shields.SetActive(false);
            _shildIsActive = false;
        }
    }

    public float GetSpawnProbability()
    {
        return _spawnProb;
    }
}
