using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobile : MonoBehaviour
{
    [Header("Player Value")]
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    private int _lives = 3;
    private bool _shieldActive = false;
    private int _score = 0;
    private Rigidbody2D _rb;

    public bool autoFire = true;


    [Header("Effects")]
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _shieldsVisualizer;
    [SerializeField]
    private GameObject _rightEngineVisualizer;
    [SerializeField]
    private GameObject _leftEngineVisualizer;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField]
    private AudioClip _laserClip;

    private Vector3 _offset = Vector3.up;
    private SpawnManagerM _spawnManager;
    private UIManagerM _uiManager;
    private AudioSource _audioSource;
    private CameraShake _cameraShake;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManagerM>();
        _audioSource = GetComponent<AudioSource>();
        _cameraShake = GameObject.Find("Main Camera").GetComponent<CameraShake>();

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

    private void Update()
    {
        CalculateMovement();

        if(autoFire)
            AutoFire();
    }

    private void CalculateMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) && transform.position.x != -2)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x - 2, transform.position.y, transform.position.z), _speed);
        }

        if (Input.GetKeyDown(KeyCode.D) && transform.position.x != 2)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x + 2, transform.position.y, transform.position.z), _speed);
        }
    }

    private void AutoFire()
    {
        if (Time.time > _canFire)
        {
            Instantiate(_laserPrefab, transform.position + _offset, Quaternion.identity);
            _canFire = Time.time + _fireRate;
        }
    }

    public void ActivateShield()
    {
        _shieldActive = true;
        _shieldsVisualizer.SetActive(_shieldActive);
    }

    public void RestoreHealth()
    {
        if (_lives < 3)
        {
            _lives++;
            _uiManager.UpdateLive(_lives);
            if (_lives == 3) _rightEngineVisualizer.SetActive(false);
            if (_lives == 2) _leftEngineVisualizer.SetActive(false);
        }
    }

    public void Damage()
    {
        _cameraShake.ActivateCameraShake();

        if (_shieldActive == true)
        {

            _shieldActive = false;
            _shieldsVisualizer.SetActive(false);
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
                SpawnManagerM.Instance.OnEndGame();
                Destroy(this.gameObject);
            }
        }
    }

    public void RemoveAmmo()
    {
        autoFire = false;
        StartCoroutine(AutoFireOffRoutine());
        
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

    IEnumerator AutoFireOffRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        autoFire = true;
    }
}
