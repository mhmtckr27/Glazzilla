using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    [SerializeField] private float InitVelocity = 10;
    [SerializeField] private float InitForce = 10;
    [SerializeField] private Rigidbody2D rigidbody;



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
            var direction = (gameObject.transform.position - Input.mousePosition).normalized;
            var magnitude = 30f *
                Vector2.Dot(direction, Input.mousePosition.normalized);
            
            var force = Vector2.Distance(gameObject.transform.position, Input.mousePosition) * ((gameObject.transform.position - Input.mousePosition).normalized);
            rigidbody.AddForceAtPosition(magnitude * direction,gameObject.transform.position,ForceMode2D.Force);
        }
    }
}
