using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public enum BossState { spawn, moveLeft, moveRight }
    [SerializeField]
    private BossState _bossState;
    [SerializeField]
    private int _enemyLives = 50;
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private int _points = 10;
    [SerializeField]
    private GameObject[] _laserPrefab;
    private float _fireRate = 2.0f;
    private float _canFire = 0.5f;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;

    private Player _player;
    private Rigidbody2D _rb;

    private UIManager _uiManager;
    private SpawnManager _spawnManager;

    private void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        _rb = GetComponent<Rigidbody2D>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

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
        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }
        if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL.");
        }
    }



    void Update()
    {
        CalculateMove();
        if (Time.time > _canFire)
        {
            FireLaser();
        }
    }


    void CalculateMove()
    {
        switch (_bossState)
        {
            case BossState.spawn:
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
                if(transform.position.y <4) 
                {
                    _bossState = BossState.moveRight;
                }
                break;

            case BossState.moveRight:

                if (transform.position.x < 8)
                {
                    transform.Translate(Vector3.right * _speed * Time.deltaTime);
                }
                else
                {
                    _bossState = BossState.moveLeft;
                }
                break;
            case BossState.moveLeft:

                if (transform.position.x > -8)
                {
                    transform.Translate(Vector3.left * _speed * Time.deltaTime);
                }
                else
                {
                    _bossState = BossState.moveRight;
                }
                break;

            default:
                break;
        }
    }

    void FireLaser()
    {
        _fireRate = Random.Range(0.5f, 1.5f);
        _canFire = Time.time + _fireRate;

        GameObject enemyLaser = Instantiate(_laserPrefab[Random.Range(0,_laserPrefab.Length)], transform.position, Quaternion.identity);
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
          

            if(_enemyLives < 1)
            {
                if (_player != null)
                {
                    _player.AddScore(_points);
                }
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                
                _speed = 0;
                _audioSource.Play();
                _uiManager.GameOverSequence(true);
                _spawnManager.OnEndGame();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 1f);
            } 

        }
        if (other.CompareTag("Laser"))
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
                _uiManager.GameOverSequence(true);
                _spawnManager.OnEndGame();
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 1f);
            }
        }
    }

}
