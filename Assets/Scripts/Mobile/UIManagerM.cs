using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
Sc

public class UIManagerM : MonoBehaviour
{



    [Header("Game UI")]
    [SerializeField]
    private GameObject _gamePanel;

    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Sprite[] _liveSprite;

    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Button _menuButton;

    private GameManager _gameManager;


    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
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
        if (currentLives < 1)
        {
            GameOverSequence();
        }
    }

    public void GameOverSequence(bool isWin = false)
    {
        if (!isWin)
        {
            _gameOverText.gameObject.SetActive(true);
            _gameManager.GameOver();
            StartCoroutine(GameOverFlickerRoutine());
        }
        else
        {
            //_restartText.gameObject.SetActive(true);
            _gameManager.GameOver();
        }
    }


    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }





}
