using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] private Vector2[] wayPoints;
    [SerializeField] private float bugSpeed;
    [SerializeField] private int direction;
    [SerializeField] private float str;
    [SerializeField] private GameState gameState;
    private bool inGlass = false;

    // Start is called before the first frame update
    void Start()
    {
        changeRotation();
    }

    public bool getInGlass()
    {
        return this.inGlass;
    }

    private void changeRotation()
    {
        if (direction == -1)
            gameObject.transform.eulerAngles = new Vector3(0, 180f, 0);
        else
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!inGlass)
            move();
        else if(inGlass)
            panic();
    }

    private void move()
    {
        if (direction == -1)
            transform.position += new Vector3(wayPoints[0].x * Time.deltaTime, 0, 0);
        else if (direction == 1)
            transform.position += new Vector3(wayPoints[1].x * Time.deltaTime, 0, 0);
        changeDirection();
    }
    private void changeDirection()
    {
        if (direction == -1 && transform.position.x < wayPoints[0].x)
            direction = 1;
        else if (direction == 1 && transform.position.x > wayPoints[1].x)
            direction = -1;
        changeRotation();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inGlass = true;

            gameState.inGlass = inGlass;
            gameState.stackNo++;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            inGlass = false;
            gameState.inGlass = inGlass;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        bump(collision);
    }

    private void bump(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            damageGlass(collision);
            direction = -direction;
            changeRotation();
        }
    }



    private void damageGlass(Collision2D collision)
    {
        collision.collider.gameObject.GetComponent<Glass>().hurt(str);
    }

    private void panic()
    {
        //if(gameState.stackNo>0)
            transform.position +=new Vector3( bugSpeed * Time.deltaTime * direction,0,0);
    }
}
