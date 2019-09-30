using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private float InitVelocity = 10;
    [SerializeField] private float InitForce = 10;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private Camera camera;

    private int screenWidthPerUnits = 9;
    private int screenHeightPerUnits = 16;


    // Start is called before the first frame update
    void Start()
    {
        //rigidbody.velocity = new Vector2(15f, 8f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("MousePos = " + mousePos);

            var dir = (transform.position - mousePos).normalized;

            /////////
            var angle = Mathf.Atan2(dir.y,dir.x);
            Debug.Log(angle);
            Debug.Log(Mathf.Rad2Deg * angle);
            var angleInDeg = Mathf.Rad2Deg * angle;
            //////////

            float magnitude = 60f;
            RaycastHit2D raycast = Physics2D.Raycast(mousePos,dir);
            var collisionPoint = raycast.point;
            RaycastHit2D hit;
            Debug.DrawRay(mousePos, transform.position - mousePos, Color.green,15f);
            LayerMask layerMask;
            //Ray ray = Physics2D.Raycast(mousePos,dir,out hit,Mathf.Infinity,layerMask);
            
            rigidbody.AddForceAtPosition(new Vector3(dir.x, 1,0)*magnitude, collisionPoint,ForceMode2D.Force);

            float clampX = rigidbody.velocity.x;
            float clampY = rigidbody.velocity.y;
            clampX = Mathf.Clamp(clampX, 0f, 0.1f);
            clampY = Mathf.Clamp(clampY, 0f, 0.1f);
            rigidbody.velocity = new Vector2(clampX, clampY);
            //rigidbody.AddTorque(Mathf.Log10(0.01f * angleInDeg )*10);
            
        }

        if (Input.GetMouseButtonDown(1))
        {
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 30;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Floor") || collision.collider.CompareTag("Bug"))
        {
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 20;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.eulerAngles = new Vector3(0, 0, 180);
    }




}
