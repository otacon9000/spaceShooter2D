using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    public enum BossState { spawn, moveLeft, moveRight, superAttack, die }
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
        
        Collider2D hit = Physics2D.OverlapCircle(transform.position, 3f);

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
             
                //super attack with prob
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
                //super attack with prob
                break;

            case BossState.superAttack:


            case BossState.die:

                break;
       

            default:
                break;

        }

    }

    void FireLaser()
    {
        _fireRate = Random.Range(1f, 2.0f);
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
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _audioSource.Play();
            //if enemyLives < 1  collide player 

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
                Destroy(GetComponent<Collider2D>());
                Destroy(this.gameObject, 1f);
            }
        }
    }

}
