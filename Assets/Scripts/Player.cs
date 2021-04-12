using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{   
    [Header("Player Value")]
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _thrusterSpeed = 10.0f;
    private float _speedMultiplier = 2f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [Header("Prefab ")]
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldsVisualizer;
    [SerializeField]
    private GameObject _rightEngineVisualizer;
    [SerializeField]
    private GameObject _leftEngineVisualizer;
    [SerializeField]
    private GameObject _explosionPrefab;

    private Vector3 _offset = Vector3.up;

    private SpawnManager _spawnManager;

    private int _score = 0;

    private bool _tripleShotActive = false;
    private bool _speedBoostActive = false;
    private bool _shieldActive = false;

    private UIManager _uiManager;
    [Header("Audio")]
    [SerializeField]
    private AudioClip _laserClip;
    private AudioSource _audioSource;

    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL.");
        }
        if(_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL.");
        }
        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL.");
        }
        else
        {
            _audioSource.clip = _laserClip;
        }

    }

    void Update()
    {
        CalculateMove();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            transform.Translate(direction * _thrusterSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        //transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.5f, 2.0f), 0);

        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        } else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }





    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_tripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + _offset, Quaternion.identity);
        }
        _audioSource.Play();
    }

    public void Damage()
    {
    
        if (_shieldActive)
        {
            _shieldActive = false;
            _shieldsVisualizer.SetActive(false);
            StopCoroutine("ShieldsRoutine");
        }
        else
        {
            _lives--;

            if (_lives == 2) _rightEngineVisualizer.SetActive(true);
            if (_lives == 1) _leftEngineVisualizer.SetActive(true);


            _uiManager.UpdateLive(_lives);
            if (_lives < 1)
            {
                Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
            }
        }

    }

    public void ActiveTripleShot()
    {
        _tripleShotActive = true;
        StartCoroutine(TripleShotRoutine());
    }

    public void ActiveSpeedBoost()
    {
        _speedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostRountine());
    }

    public void ActivateShield()
    {
        _shieldActive = true;
        _shieldsVisualizer.SetActive(true);
        StartCoroutine(ShieldsRoutine());
    }

   
    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    IEnumerator TripleShotRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotActive = false;
    }

    IEnumerator SpeedBoostRountine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostActive = false;
        _speed /= _speedMultiplier;
    }

    IEnumerator ShieldsRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _shieldActive = false;
        _shieldsVisualizer.SetActive(false);
    }
}
