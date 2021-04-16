using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum typeOfEnemy { normal ,scoreEnemy,aggressive,boss };
    [SerializeField]
    private typeOfEnemy _enemyType;
    [SerializeField]
    private int _enemylife = 1;
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private int _points = 10;
    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private float _fireRate = 3.0f;
    private float _canFire = 0.5f;

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
        //Physics2D.queriesStartInColliders = false;
    }

   

    void Update()
    {
        Move();

        

        if(Time.time > _canFire && _enemyType != typeOfEnemy.aggressive)
        {
            FireLaser();
        }

        if (transform.position.x > 13.5f)
            Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            
            
            if(_player != null)
            {
                _player.Damage();
            }
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,1f);
        }
        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

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

    void Move()
    {
        RaycastHit2D hit = Physics2D.Raycast((transform.position + Vector3.down), Vector2.down, 10.0f);
        
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
                if (hit.collider != null && hit.collider.CompareTag("Player"))
                {
                    transform.Translate(Vector3.down * _speed * 3 * Time.deltaTime);
                }

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
}
