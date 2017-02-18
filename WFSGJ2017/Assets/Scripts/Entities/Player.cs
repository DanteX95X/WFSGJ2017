using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	#region variables
	bool isJumping;

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
			Jump();
		}
	}

	void Jump()
	{
		if (isJumping)
			return;

		isJumping = true;
		GetComponent<Rigidbody2D>().AddForce(new Vector2(100, jumpForceY));
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject collider = collision.collider.gameObject;
		if (collider.tag == "obstacle" )
		{
			if (collider.transform.position.y < transform.position.y)
			{
				if ( transform.position.x < (collider.transform.position.x + collider.transform.localScale.x / 2) 
					&& transform.position.x > (collider.transform.position.x - collider.transform.localScale.x / 2) 
					)
				{
					Debug.Log("Jump reset");
					isJumping = false;
				}
				else
				{
					Debug.Log("Reflected");
					Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
					rigidbody.AddForce(new Vector2(-100, jumpForceY));
				}
			}
			else
			{
				
			}
		}
	}

	#endregion methods
}