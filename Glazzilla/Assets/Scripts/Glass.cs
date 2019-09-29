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
            //var direction = (gameObject.transform.localPosition - Input.mousePosition).normalized;
            //direction = -direction.normalized;
            //var magnitude = 30f *
            //Vector2.Dot(direction, Input.mousePosition.normalized);

            //var force = Vector2.Distance(gameObject.transform.localPosition, Input.mousePosition) * ((gameObject.transform.localPosition - Input.mousePosition).normalized);
            //rigidbody.AddForceAtPosition(magnitude * direction,gameObject.transform.localPosition,ForceMode2D.Force);

            //Debug.Log(gameObject.transform.localPosition);
            //Debug.Log(gameObject.transform.position);
            //Debug.Log(Input.mousePosition);
            //Debug.Log((gameObject.transform.position - Input.mousePosition).normalized);
            //Debug.Log(gameObject.transform.InverseTransformPoint(Input.mousePosition));
            //GetComponent<Rigidbody2D>().AddForce(direction * magnitude);



            //var mouse = new Vector2(Input.mousePosition.x / Screen.width*screenWidthPerUnits, Input.mousePosition.y / Screen.height * screenHeightPerUnits);
            var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log("MousePos = " + mousePos);

            var dir = (transform.position - mousePos).normalized;
            float magnitude = 300f;

            GetComponent<Rigidbody2D>().AddForce(dir * magnitude);
            //Debug.Log(dir);
            //Debug.Log(mouse);

            //Debug.Log(gameObject.transform.position.);
            

            //var magn = 50f;
            //var mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            
            
           
            //var dir2 = gameObject.transform.InverseTransformDirection(dir);
            //dir2.Normalize();

            //GetComponent<Rigidbody2D>().AddForce(magn * -dir2);
        }
    }
}
