using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	#region variables
	bool isJumping;

	float jumpTimer = 0;
	public float jumpScale = 1.0f;
	public float minJumpPressingTime = 1.0f;
	public float maxJumpPressingTime = 8.0f;

	[SerializeField]
	float jumpForceY;

	#endregion

	#region properties

	#endregion

	#region methods

	void Start ()
	{
		isJumping = true;
		jumpForceY = 300;
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Jump(jumpForceY);
		}

		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			jumpTimer = Time.time;

		}
		else if (Input.GetKeyUp(KeyCode.Mouse0) || (Input.GetKey(KeyCode.Mouse0) && (Time.time - jumpTimer) > maxJumpPressingTime))
		{
			float deltaTime = Time.time - jumpTimer;

			float scale = 0.0f;
			if (deltaTime > minJumpPressingTime)
			{
				scale += Mathf.Clamp(deltaTime, 0, maxJumpPressingTime) / maxJumpPressingTime;
			}

			Jump(jumpForceY + jumpForceY * scale * jumpScale * (maxJumpPressingTime - minJumpPressingTime));

			jumpTimer = Time.time;
		}
	}

	void Jump(float forceY)
	{
		if (isJumping)
			return;

		isJumping = true;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forceY));
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject collider = collision.collider.gameObject;
		if (collider.tag == "obstacle" )
		{
			//Debug.Log((collider.transform.position.y + collider.transform.localScale.y / 2));
			//Debug.Log((transform.position.y - transform.localScale.y / 2));
			if (collider.transform.position.y < transform.position.y)
			{
				//if((transform.position.x - transform.localScale.x) > transform.position )
				Debug.Log("Jump reset");
				isJumping = false;
			}
			else
			{
				//Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
				//rigidbody.velocity = new Vector2(-rigidbody.velocity.x, rigidbody.velocity.y);
			}
		}
	}

	#endregion methods
}