using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
	[SerializeField] private float InitVelocity = 10;
	[SerializeField] private float InitForce = 10;
	[SerializeField] float magnitude = 30f;
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
		if (transform.rotation.z > 360)
			Debug.LogError("Vall yle");
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

		RaycastHit2D raycast = Physics2D.Raycast(mousePos, dir);
		var collisionPoint = raycast.point;

		////					
		int rotInd = FindRotation(transform.eulerAngles.z);
		float x;
		float y;

		switch (rotInd)
		{
			case 1:
				x=dir.x;
				y=1;
				//Debug.LogError("asd");
			break;
				
			case 2:
				x=dir.x;
				y=1;
			break;

			case 3:
				x=dir.x;
				y=-1;
			break;

			case 4:
				x=dir.x;
				y=-1;
			break;

			case 5:
				x=0;
				y=1;
			break;

			case 6:
				x=dir.x;
				y=-1;
			break;

			case 7:
				x=dir.x;
				y=-1;
			break;

			case 8:
				x=dir.x;
				y=1;
			break;

			default:
				x=0;
				y=0;
			break;
		}



		////

		rigidbody.AddForceAtPosition(new Vector3(x*Mathf.PI/2,y , 0) * magnitude, collisionPoint, ForceMode2D.Force);
		//rigidbody.AddForce(new Vector2(0,y*magnitude));

		float clampX = rigidbody.velocity.x;
		float clampY = rigidbody.velocity.y;
		clampX = Mathf.Clamp(clampX, 0f, 0.1f);
		clampY = Mathf.Clamp(clampY, 0f, 0.1f);
		rigidbody.velocity = new Vector2(clampX, clampY);

	}

#pragma warning disable MS002 // Cyclomatic Complexity does not follow metric rules.
	private int FindRotation(float rot)
#pragma warning restore MS002 // Cyclomatic Complexity does not follow metric rules.
	{
		int rotInd = 0;
		if (rot <= 10 && rot > -10)
		{
			rotInd = 1;
		}

		else if (rot > 280 && rot <= 350)
		{
			rotInd = 2;
		}

		else if (rot <= 280 && rot > 260)
		{
			rotInd = 3;
		}

		else if (rot <= 260 && rot > 190)
		{
			rotInd = 4;
		}

		else if (rot <= 190 && rot > 170)
		{
			rotInd = 5;
		}

		else if (rot <= 170 && rot > 100)
		{
			rotInd = 6;
		}

		else if (rot <= 100 && rot > 80)
		{
			rotInd = 7;
		}

		else if (rot <= 80 && rot > 10)
		{
			rotInd = 8;
		}


		return rotInd;
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





}
