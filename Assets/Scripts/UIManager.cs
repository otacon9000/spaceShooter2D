using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Image _livesImage;
    [SerializeField]
    private Sprite[] _liveSprite; 
  
    void Start()
    {
        _scoreText.text = "Score: "+0;
    }



    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLive(int currentLives)
    {
        _livesImage.sprite = _liveSprite[currentLives];
    }
    
}
