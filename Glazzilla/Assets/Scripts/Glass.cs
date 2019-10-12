using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
	[SerializeField] float magnitude = 30f;
	[SerializeField] private Rigidbody2D rigidbody;
	[SerializeField] private Camera camera;
    [SerializeField] private PolygonCollider2D innerCollider;
	[SerializeField] private GameState gameState;
    [SerializeField] private float HP;
    private bool inUse;
    private bool isStack;

	// Start is called before the first frame update
	void Start()
	{
		rigidbody = GetComponent<Rigidbody2D>();
        
	}
    public void setGameState(GameState gameState)
    {
        this.gameState = gameState;
    }
	// Update is called once per frame
	void Update()
	{
		if (transform.rotation.z > 360)
			Debug.LogError("Vall yle");
		if (Input.GetMouseButtonDown(0))
        {
            rigidbody.bodyType = RigidbodyType2D.Dynamic;
            bounce();
        }

			
		if (Input.GetMouseButtonDown(1))
			strike();
	}

	private void strike()
	{
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
		gameObject.GetComponent<Rigidbody2D>().freezeRotation = true;
		gameObject.GetComponent<Rigidbody2D>().gravityScale = 30;
        inUse = true;
    }

	private void bounce()
	{
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        inUse = true;
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		var dir = (transform.position - mousePos).normalized;

		RaycastHit2D raycast = Physics2D.Raycast(mousePos, dir);
		var collisionPoint = raycast.point;
        collisionPoint = innerCollider.bounds.ClosestPoint(mousePos);
        ////					
        int rotInd = FindRotation(transform.eulerAngles.z);
		float x;
		float y;

		switch (rotInd)
		{
			case 1:
				x=dir.x*0.25f;
				y=1;
			break;
				
			case 2:
				x=dir.x*2;
				y=1;
			break;

			case 3:
				x=dir.x;
				y=-1;
			break;

			case 4:
				x=dir.x*0.5f;
				y=-1;
                
                Debug.Log(collisionPoint);
                //collisionPoint.x += 0.1f;
			break;

			case 5:
				x=0;
				y=1;
			break;

			case 6:
				x=dir.x*0.5f;
				y=-1;
			break;

			case 7:
				x=dir.x;
				y=-1;
			break;

			case 8:
				x=dir.x*2;
				y=1;
			break;

			default:
                Debug.LogError("asd");
				x=0;
				y=0;
			break;
		}

        Debug.DrawRay(mousePos, transform.position - mousePos,Color.green,2f);
		////

		rigidbody.AddForceAtPosition(new Vector3(x*Mathf.PI/2.2f,y , 0) * magnitude, collisionPoint, ForceMode2D.Force);
        if(y==-1)
            rigidbody.AddForce(new Vector2(0,-2*y*magnitude));

		float clampX = rigidbody.velocity.x;
		float clampY = rigidbody.velocity.y;
		clampX = Mathf.Clamp(clampX, 0f, 0.1f);
		clampY = Mathf.Clamp(clampY, 0f, 0.1f);
		rigidbody.velocity = new Vector2(clampX, clampY);

	}


	private int FindRotation(float rot)

	{
		int rotInd = 0;
        Debug.Log(rot);
        if (rot < 0)
            rot = 360 + rot;

		if (rot <= 10 || rot-360 > -10)
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

        Debug.Log(rotInd);
		return rotInd;
	}

    public void hurt(float str)
    {
        HP -= str;
        if(HP<=0)
            glassBreak();
    }

    private void glassBreak()
    {
        gameState.stackNo--;
        Destroy(gameObject);
    }

    public void spawnGlass()
    {
        gameState.GetComponent<GameState>().spawnGlass();
    }
    private IEnumerator Delay(float duration)
    {
        yield return new WaitForSeconds(duration);
        if(!gameState.inGlass)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(!isStack)
                gameState.stackNo++;
            isStack = true;
        }
        if (collision.gameObject.CompareTag("Bug"))
        {
            isStack = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isStack = false;
            if(gameState.stackNo > 0)
            {
                gameState.inGlass = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
	{
		if ((collision.collider.CompareTag("Floor") || collision.collider.CompareTag("Bug") || collision.collider.CompareTag("Player")) && inUse)
		{
            inUse = false;
            
			gameObject.GetComponent<Rigidbody2D>().gravityScale = 20;
			gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            if (!collision.collider.gameObject.GetComponent<Bug>() && !isStack)
            {
                StartCoroutine("Delay", 1f);
            }
                
			GetComponent<Glass>().enabled = false;
                if (!inUse)
                {
                    gameState.GetComponent<GameState>().setIsinstantiated(false);
                    spawnGlass();
                }
		}
	}





}
