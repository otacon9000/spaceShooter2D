using System.Collections;
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
            SceneManager.LoadScene(0); //current game scene 
            Debug.Log("RRRRR");
        }
        
    }


    public void GameOver()
    {
        _isGameOver = true;
    }
}
