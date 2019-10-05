using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private float InitVelocity = 10;
    [SerializeField] private float InitForce = 10;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Camera camera;
    [SerializeField] private GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            bounce();
        if (Input.GetMouseButtonDown(1))
            strike();
    }

    private void strike()
    {
        gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 30;
    }

    private void bounce()
    {

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var dir = (transform.position - mousePos).normalized;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            float magnitude = 30f;
            RaycastHit2D raycast = Physics2D.Raycast(mousePos, dir);
            var collisionPoint = raycast.point;
            Debug.DrawRay(mousePos, transform.position - mousePos, Color.green, 15f);
            rigidbody.AddForceAtPosition(new Vector3(dir.x * Mathf.PI/2 , 1, 0) * magnitude, collisionPoint, ForceMode2D.Force);
             
            float clampX = rigidbody.velocity.x;
            float clampY = rigidbody.velocity.y;
            clampX = Mathf.Clamp(clampX, 0f, 0.1f);
            clampY = Mathf.Clamp(clampY, 0f, 0.1f);
            rigidbody.velocity = new Vector2(clampX, clampY);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor") || collision.collider.CompareTag("Bug"))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 20;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            GetComponent<Glass>().enabled = false;

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        gameState.win();
    }

}
