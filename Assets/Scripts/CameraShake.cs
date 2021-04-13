using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{

    [SerializeField]
    private float _shakeIntensity = 0.5f;
    [SerializeField]
    private float _shakeDuration = 1.5f;
    private float _shakeTime = 0.0f;
    [SerializeField]
    private float _dampingSpeed = 0.5f;
    
    private Vector3 _startPosition;


    void Awake()
    {
        _startPosition = transform.localPosition;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.K)) ActivateCameraShake();

        if (_shakeTime > 0)
        {
            transform.localPosition = _startPosition + Random.insideUnitSphere * _shakeIntensity;

            _shakeTime -= _dampingSpeed * Time.deltaTime;
        }
        else
        {
            _shakeTime = 0;
            transform.localPosition = _startPosition;
        }
    }

    public void ActivateCameraShake()
    {
        _shakeTime = _shakeDuration;
    }

}
