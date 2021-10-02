﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private bool _isGameOver = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && _isGameOver)
        {
            SceneManager.LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.Escape) && _isGameOver)
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.M) && _isGameOver)
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    public void RestartGame()
    {
        //SceneManager.LoadScene("MobileGame");
        Debug.Log("Restart button pressed");
    }

    public void GoBackToMenu()
    {
        //main menu
    }


}
