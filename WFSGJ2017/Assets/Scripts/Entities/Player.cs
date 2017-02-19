using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	#region variables
	bool isJumping;
	Vector3 jumpDirection;
	Camera mainCamera;
	Transform renderer;


	float jumpTimer = 0;
	public float jumpScale = 1.0f;
	public float minJumpPressingTime = 1.0f;
	public float maxJumpPressingTime = 8.0f;

	public float liftJumpPower =100;
	public float liftSpeed = 0.005f;

	bool loadJump = false;

	[SerializeField]
	float jumpForce = 0;

	#endregion

	#region properties

	#endregion

	#region methods

	void Start()
	{
		isJumping = true;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		renderer = this.gameObject.transform.GetChild(0);
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//Jump(jumpForce);
			Destroy(gameObject);
		}

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			jumpTimer = Time.time;
			loadJump = true;
		}
		else if (loadJump && (Input.GetKeyUp(KeyCode.Mouse0) || (Input.GetKey(KeyCode.Mouse0) && (Time.time - jumpTimer) > maxJumpPressingTime)))
		{
			float deltaTime = Time.time - jumpTimer;
			float scale = 0.0f;
			if (deltaTime > minJumpPressingTime)
			{
				scale += Mathf.Clamp(deltaTime, 0, maxJumpPressingTime) / maxJumpPressingTime;
			}

			Jump(jumpForce + jumpForce * scale * jumpScale * (maxJumpPressingTime - minJumpPressingTime));


			jumpTimer = Time.time;
			loadJump = false;
		}
		if (loadJump)
		{
			renderer.localScale = Vector3.Lerp(new Vector3(1, 1, 1), new Vector3(1, 0.5f, 1), (Time.time - jumpTimer) / maxJumpPressingTime);
			renderer.localPosition = Vector3.Lerp(new Vector3(0, 0, 0), new Vector3(0, 0 - 0.3f, 0), (Time.time - jumpTimer) / maxJumpPressingTime);
		}
	}
	void FixedUpdate()
	{
		RotationCheck();
	}
	void RotationCheck()
	{
		float zRot = this.transform.rotation.eulerAngles.z;
		if (zRot > 5 && zRot < 355 && GetComponent<Rigidbody2D>().velocity.magnitude < liftSpeed)
		{
			Debug.Log("Poprawka! " + zRot);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0,liftJumpPower));
			this.transform.Rotate(-this.transform.rotation.eulerAngles);
		}
	}

    void Jump(float force)
    {
        if (isJumping)
            return;
		GetComponent<AudioSource>().Play();

		renderer.localScale = new Vector3(1, 1, 1);
		renderer.localPosition = new Vector3(0, 0, 0);
		jumpDirection = Input.mousePosition;

		jumpDirection.z = transform.position.z - mainCamera.transform.position.z;
		jumpDirection = mainCamera.ScreenToWorldPoint(jumpDirection);
		jumpDirection = (jumpDirection - transform.position).normalized * force;
		if (jumpDirection.y > 20)
		{
			//isJumping = true;
			GetComponent<Rigidbody2D>().AddForce(new Vector2(jumpDirection.x, jumpDirection.y));
		}

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
		GameObject collider = collision.collider.gameObject;
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
		Rigidbody2D colliderRigidbody = collider.GetComponent<Rigidbody2D>();
		if (collider.tag == "obstacle" || collider.tag == "jellyObstacle")
		{
			if (collider.transform.position.y < transform.position.y)
			{
				if ( rigidbody.position.x < (colliderRigidbody.position.x + collider.transform.localScale.x / 2) 
					&& rigidbody.position.x > (colliderRigidbody.position.x - collider.transform.localScale.x / 2) 
					)
				{
					isJumping = false;
				}
			}
		}
		if(collider.tag == "jellyObstacle")
		{
			if (
				((rigidbody.position.y + transform.localScale.y / 2 < (colliderRigidbody.position.y + collider.transform.localScale.y / 2) && rigidbody.position.y > (colliderRigidbody.position.y - collider.transform.localScale.y / 2))
				|| (rigidbody.position.y - transform.localScale.y / 2 < (colliderRigidbody.position.y + collider.transform.localScale.y / 2) && rigidbody.position.y > (colliderRigidbody.position.y - collider.transform.localScale.y / 2)))
				&&
				(rigidbody.position.x > (colliderRigidbody.position.x + collider.transform.localScale.x / 2) || rigidbody.position.x < (colliderRigidbody.position.x - collider.transform.localScale.x / 2))
				)
			{
				rigidbody.AddForce(new Vector2(-jumpDirection.x, jumpDirection.y));
			}
			else if (
					((rigidbody.position.x + transform.localScale.x / 2 < (colliderRigidbody.position.x + collider.transform.localScale.x / 2) && rigidbody.position.x > (colliderRigidbody.position.x - collider.transform.localScale.x / 2))
					|| (rigidbody.position.x - transform.localScale.x / 2 < (colliderRigidbody.position.x + collider.transform.localScale.x / 2) && rigidbody.position.x > (colliderRigidbody.position.x - collider.transform.localScale.x / 2)))
					&&
					(/*rigidbody.position.y > (colliderRigidbody.position.y + collider.transform.localScale.y / 2) ||*/ rigidbody.position.y < (colliderRigidbody.position.y - collider.transform.localScale.y / 2))
					)
			{
				rigidbody.AddForce(new Vector2(jumpDirection.x, -jumpDirection.y));
			}
			else if (
					((rigidbody.position.x + transform.localScale.x / 2 < (colliderRigidbody.position.x + collider.transform.localScale.x / 2) && rigidbody.position.x > (colliderRigidbody.position.x - collider.transform.localScale.x / 2))
					|| (rigidbody.position.x - transform.localScale.x / 2 < (colliderRigidbody.position.x + collider.transform.localScale.x / 2) && rigidbody.position.x > (colliderRigidbody.position.x - collider.transform.localScale.x / 2)))
					&&
					(rigidbody.position.y > (colliderRigidbody.position.y + collider.transform.localScale.y / 2) /*|| rigidbody.position.y < (colliderRigidbody.position.y - collider.transform.localScale.y / 2)*/)
					)
			{
				rigidbody.AddForce(new Vector2(jumpDirection.x, jumpDirection.y));
			}
		}
	}

	void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.collider.gameObject.tag == "obstacle" || collision.collider.gameObject.tag == "jellyObstacle")
		{
			isJumping = true;
		}
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		GameObject collider = collision.collider.gameObject;
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
		Rigidbody2D colliderRigidbody = collider.GetComponent<Rigidbody2D>();
		//if (collider.tag == "obstacle" || collider.tag == "jellyObstacle")
		//{
			if (collider.transform.position.y < transform.position.y)
			{
				if (
					(rigidbody.position.x + transform.localScale.x < (colliderRigidbody.position.x + collider.transform.localScale.x / 2)
					&& rigidbody.position.x + transform.localScale.x > (colliderRigidbody.position.x - collider.transform.localScale.x / 2))
					||
					(rigidbody.position.x - transform.localScale.x < (colliderRigidbody.position.x + collider.transform.localScale.x / 2)
					&& rigidbody.position.x - transform.localScale.x > (colliderRigidbody.position.x - collider.transform.localScale.x / 2))
					)
				{
					isJumping = false;
				}
			}
		//}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.gameObject.tag == "checkpoint")
		{
			mainCamera.GetComponent<Game>().NextCheckpoint(collider.gameObject);
		}
		Debug.Log(collider.gameObject.tag);
		if (collider.gameObject.tag == "collectible")
		{
			mainCamera.GetComponent<Game>().GainLife(collider.transform.GetChild(0).gameObject);
			Destroy(collider.gameObject);
		}
	}

	#endregion methods
}
