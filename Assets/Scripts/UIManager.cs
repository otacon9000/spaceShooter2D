﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _ammoText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Slider _thrusterSlider;
    [SerializeField]
    private Text _gameOverText;
    [SerializeField]
    private Text _youWinText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Sprite[] _liveSprite;
  
    private GameManager _gameManager;

  
    void Start()
    {
        _scoreText.text = "Score: "+0;
        _gameOverText. gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("Game Manager is NULL.");
        }
    }


    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLive(int currentLives)
    {
        if (currentLives == -1) return;
        _livesImage.sprite = _liveSprite[currentLives];
        if(currentLives < 1)
        {
            GameOverSequence();
        }
    }

    public void UpdateAmmoCounter(int ammoCounter, int maxAmmo)
    {
        _ammoText.text =  ammoCounter.ToString() + " / " + maxAmmo.ToString() ;
    }

    public void GameOverSequence(bool isWin = false)
    {
        if (!isWin)
        {
            _gameOverText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
            _gameManager.GameOver();
            StartCoroutine(GameOverFlickerRoutine());
        }
        else
        {
            _youWinText.gameObject.SetActive(true);
            _restartText.gameObject.SetActive(true);
            _gameManager.GameOver();
            StartCoroutine(YouWinFlickerRoutine());
        }
    }

    public void SetMaxThrusterValue(float thrusterValue)
    {
        _thrusterSlider.maxValue = thrusterValue;
        _thrusterSlider.value = thrusterValue;
    }

    public void UpdateThrusterBar(float thrusterValue)
    {
        _thrusterSlider.value = thrusterValue;
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
    IEnumerator YouWinFlickerRoutine()
    {
        while (true)
        {
           _youWinText.text = "YOU WIN";
            yield return new WaitForSeconds(0.5f);
            _youWinText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }
}
