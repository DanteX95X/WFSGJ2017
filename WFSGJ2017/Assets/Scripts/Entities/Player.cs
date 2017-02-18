using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

	#region variables
	bool isJumping;
	Vector3 jumpDirection;
	Camera mainCamera;


	float jumpTimer = 0;
	public float jumpScale = 1.0f;
	public float minJumpPressingTime = 1.0f;
	public float maxJumpPressingTime = 8.0f;

	bool loadJump = false;

	[SerializeField]
	float jumpForce;

	#endregion

	#region properties

	#endregion

	#region methods

	void Start()
	{
		isJumping = true;
		jumpForce = 300;
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump(jumpForce);
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
	}

    void Jump(float force)
    {
        if (isJumping)
            return;

		jumpDirection = Input.mousePosition;

		jumpDirection.z = transform.position.z - mainCamera.transform.position.z;
		jumpDirection = mainCamera.ScreenToWorldPoint(jumpDirection);
		jumpDirection = (jumpDirection - transform.position).normalized * force;
		if (jumpDirection.y > 0)
		{
			isJumping = true;
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

	#endregion methods
}
