using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{   
    [Header("Player Value")]
    [SerializeField]
    private float _speed = 5.0f;
    private float _currentSpeed = 5.0f;
    [SerializeField]
    private float _thrusterSpeed = 10.0f;
    private float _currentThruster = 20.0f;
    [SerializeField]
    private float _thrusterMaxValue = 10.0f;
    [SerializeField]
    private float _thrusterConsume = 1.5f;
    private float _speedMultiplier = 2f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private int _lives = 3;
    private int _currentAmmo = 15;
    [SerializeField]
    private int _maxAmmo = 15;

    [Header("Effects")]
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _spreadShootPrefab;
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

    private CameraShake _cameraShake;

    private int _score = 0;

    private bool _tripleShotActive = false;
    private bool _spreadShootActive = false;
    private bool _speedBoostActive = false;
    private bool _shieldActive = false;
    private int _shieldLives = 2;
    private bool _fillThruster = false;
    private bool _attractiveRadius = false;

    private UIManager _uiManager;
    [Header("Audio")]
    [SerializeField]
    private AudioClip _laserClip;
    [SerializeField]
    private AudioClip _noBulletClip;
    private AudioSource _audioSource;

    void Awake()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();
        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

        if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is NULL.");
        }
        if (_uiManager == null)
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
        if (_cameraShake == null)
        {
            Debug.LogError("The CaneraShake is NULL.");
        }
    }

    void Start()
    {
        _currentSpeed = _speed;
        _currentAmmo = _maxAmmo;
        _currentThruster = _thrusterMaxValue;
        _uiManager.SetMaxThrusterValue(_thrusterMaxValue);
        StartCoroutine(ThrusterRechargeRoutine());
    }

    void Update()
    {
        CalculateMove();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }

        Collider2D [] hit = Physics2D.OverlapCircleAll(transform.position, 10f);

        if(Input.GetKey(KeyCode.C))
        {
            _attractiveRadius = true;
            foreach (Collider2D pickupCollider in hit)
            {

                if (pickupCollider.CompareTag("PowerUp"))
                {
                    StartCoroutine(AttractivePickupRoutine(pickupCollider));
                }
            }
        }
        else
        {
            _attractiveRadius = false;
            //StopCoroutine("AttractivePickupRoutine");
        }

    }

    void CalculateMove()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);


        if (Input.GetKey(KeyCode.LeftShift) && _currentThruster > 0 )
        {
            _fillThruster = false;
            _currentSpeed = _thrusterSpeed;
            transform.Translate(direction * _currentSpeed * Time.deltaTime);
            _currentThruster -= _thrusterConsume * Time.deltaTime;
            _uiManager.UpdateThrusterBar(_currentThruster);
        }
        else
        {
            _fillThruster = true;
            _currentSpeed = _speed;
            transform.Translate(direction * _currentSpeed * Time.deltaTime);
        }

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
        if (_currentAmmo > 1)
        {
            _currentAmmo--;
            _uiManager.UpdateAmmoCounter(_currentAmmo, _maxAmmo);
            _audioSource.clip = _laserClip;
            _canFire = Time.time + _fireRate;
            if (_tripleShotActive || _spreadShootActive)
            {
                if (_tripleShotActive)
                {
                    Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(_spreadShootPrefab, transform.position, Quaternion.identity);
                }
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + _offset, Quaternion.identity);
            }
            _audioSource.PlayOneShot(_laserClip);
        }
        else
        {
            _uiManager.UpdateAmmoCounter(0, _maxAmmo);
            _audioSource.PlayOneShot(_noBulletClip);
        }
    }

    public void Damage()
    {
        _cameraShake.ActivateCameraShake();

        if (_shieldActive)
        {
            if (_shieldLives <1)
            {
                _shieldActive = false;
                _shieldsVisualizer.SetActive(false);
            }
            else
            {
                _shieldLives--;
                if (_shieldLives == 1)
                {
                    _shieldsVisualizer.GetComponent<SpriteRenderer>().color = Color.green;
                }
                if (_shieldLives == 0)
                {
                    _shieldsVisualizer.GetComponent<SpriteRenderer>().color = Color.red;
                }
            }
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
        _currentSpeed *= _speedMultiplier;
        StartCoroutine(SpeedBoostRountine());
    }

    public void ActivateShield()
    {
        _shieldLives = 2;
        _shieldActive = true;
        _shieldsVisualizer.GetComponent<SpriteRenderer>().color = Color.white;
        _shieldsVisualizer.SetActive(true);
    }

    public int GetMaxAmmo()
    {
        return _maxAmmo;
    }

    public void AddAmmo()
    {
        _currentAmmo = _maxAmmo;
        _uiManager.UpdateAmmoCounter(_currentAmmo, _maxAmmo);
    }

    public void RestoreHealth()
    {
        if(_lives < 3)
        {
            _lives++;
            _uiManager.UpdateLive(_lives);
            if (_lives == 3) _rightEngineVisualizer.SetActive(false);
            if (_lives == 2) _leftEngineVisualizer.SetActive(false);
        }
    }

    public void ActivateSpreadShot()
    {
        _spreadShootActive = true;
        StartCoroutine(SpreadShotRoutine());
    }

    public void RemoveAmmo()
    {
        _currentAmmo = 0;
        _uiManager.UpdateAmmoCounter(_currentAmmo, _maxAmmo);

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
        _currentSpeed /= _speedMultiplier;
    }

    IEnumerator SpreadShotRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _spreadShootActive = false;
    }

    IEnumerator ThrusterRechargeRoutine()
    {
        while(true)
        {
            if(_currentThruster < _thrusterMaxValue && _fillThruster == true)
            {
                _currentThruster += 0.01f;
                _uiManager.UpdateThrusterBar(_currentThruster);
            }
            yield return null;
        }
    }

    IEnumerator AttractivePickupRoutine(Collider2D hit)
    {
        
        while(_attractiveRadius)
        {

            if(hit != null)
            {
                hit.transform.position = Vector3.Lerp(hit.transform.position, transform.position, Time.deltaTime / 10);
            }
                
            yield return null;
        }
    }
        
    
}
