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
        //rigidbody.velocity = new Vector2(3f, 2f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {




            

            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("MousePos = " + mousePos);

            var dir = (transform.position - mousePos).normalized;
            float magnitude = 50f;
            RaycastHit2D raycast = Physics2D.Raycast(mousePos,dir);
            var collisionPoint = raycast.point;
            RaycastHit2D hit;
            Debug.DrawRay(mousePos, transform.position - mousePos, Color.green,15f);
            LayerMask layerMask;
            //Ray ray = Physics2D.Raycast(mousePos,dir,out hit,Mathf.Infinity,layerMask);
            
            rigidbody.AddForceAtPosition(dir * magnitude, collisionPoint,ForceMode2D.Force);
            float clampX = rigidbody.velocity.x;
            float clampY = rigidbody.velocity.y;
            clampX = Mathf.Clamp(clampX, 0f, 0.1f);
            clampY = Mathf.Clamp(clampY, 0f, 0.1f);
            rigidbody.velocity = new Vector2(clampX, clampY);

        }

        

    }


}
