using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bug : MonoBehaviour
{
    [SerializeField] private Vector2[] wayPoints;
    [SerializeField] private float bugSpeed;
    [SerializeField] private int direction;

    private void Start()
    {
        if (direction == -1)
            gameObject.transform.eulerAngles = new Vector3(0, 180f, 0);
        else
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
    }

    //private void update()
    //{
    //    if (gameobject.transform.rotation.y == 0)
    //    {
    //        direction =
    //    }
    //    gameobject.getcomponent<rigidbody2d>().velocity = new vector2(bugspeed, 0);
    //    gameobwaypoints[0]
    //}

}
