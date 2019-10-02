using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    [SerializeField] private Vector2[] wayPoints;
    [SerializeField] private float bugSpeed;
    [SerializeField] private int direction;
    private bool inGlass = false;

    // Start is called before the first frame update
    void Start()
    {
        changeRotation();
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            inGlass = true;
            direction = -direction;
            changeRotation();
            
        }

    }

    private void panic()
    {
        transform.position +=new Vector3( bugSpeed * Time.deltaTime * direction,0,0);
    }
}
