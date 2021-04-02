﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField]
    private AudioClip _explosionClip;
    private AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        if (_audioSource == null)
        {
            Debug.LogError("The AudioSource is NULL.");
        }
        else
        {
            _audioSource.clip = _explosionClip;
        }
        _audioSource.Play();
        Destroy(this.gameObject, 3.0f);
    }

}
