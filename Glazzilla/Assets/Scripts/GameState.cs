using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField] private Glass glass;
    private bool Isinstantiated = false;
    public bool inGlass { get; set; }
    [SerializeField] public float timeLeft = 0;
    public bool winState { get; set; }
    private bool isGameOver;
    [SerializeField] public int stackNo { get; set; }

    private void Start()
    {
        winState = false;
        isGameOver = false;
    }

    private void Update()
    {
        Debug.Log(stackNo);
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0 && !isGameOver)
            gameOver();
    }

    private void gameOver()
    {
        isGameOver = true;
        if (inGlass)
            winState = true;
        else
            winState = false;
        SceneManager.LoadScene(2);
        DontDestroyOnLoad(gameObject);
    }

    public void setIsinstantiated(bool Isinstantiated)
    {
        this.Isinstantiated = Isinstantiated;
    }

    public void spawnGlass()
    {
        if (!Isinstantiated)
        {
            
            var newGlass = Instantiate(glass, new Vector3(0, 0, 0), Quaternion.Euler(0,0,180));
            newGlass.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            newGlass.GetComponent<Glass>().setGameState(this);
            newGlass.transform.localScale = new Vector3(1, 1, 1);
            Isinstantiated = true;
            
            
        }
        
    }


}
