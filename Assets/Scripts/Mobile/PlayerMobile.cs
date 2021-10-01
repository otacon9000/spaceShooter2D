using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMobile : MonoBehaviour
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

    private Vector3 _offset = Vector3.up;

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

   
}
