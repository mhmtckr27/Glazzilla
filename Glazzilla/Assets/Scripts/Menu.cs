using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    private string winState;
    [SerializeField] private GameState gameState;
    [SerializeField] private Text winStateText;

    private void Start()
    {
        gameState = FindObjectOfType<GameState>();
        if (winStateText)
        {
            if (gameState.GetComponent<GameState>().winState)
                winStateText.text = "You Win!";
            else
                winStateText.text = "You Lose!";
        }
        
    }

    public void changeLevel(int i)
    {
        SceneManager.LoadScene(i);
    }



    public void exit()
    {
        Application.Quit();
    }
}
