using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public enum typeOfEnemy { normalEnemy ,scoreEnemy,bossEnemy };
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
    private float _fireRate = 3.0f;
    private float _canFire = 0.5f;

    private Animator _anim;

    private Player _player;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();


        if (_player == null)
        {
            Debug.LogError("The player is NULL.");
        }
        
        if (_anim == null)
        {
            Debug.LogError("The Animator is NULL.");
        }

        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL.");
        }
        else
        {
            _audioSource.clip = _explosionClip;
        }
    }

    void Update()
    {
        Move();

        

        if(Time.time > _canFire)
        {
            FireLaser();
        }

        if (transform.position.x > 13)
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
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
        if(other.CompareTag("Laser"))
        {
            Destroy(other.gameObject);

            if (_player != null)
            {
                _player.AddScore(_points);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
    }

    void Move()
    {
        switch (_enemyType) 
        {
            case typeOfEnemy.normalEnemy:
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
