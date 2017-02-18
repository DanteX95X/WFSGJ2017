using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
	#region variables

	#endregion

	#region methods

	void Start ()
	{
		
	}
	
	
	void Update ()
	{
		
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.collider.gameObject.tag == "Player" && GetComponent<Rigidbody2D>().velocity.y < -0.5)
		{
			Destroy(collision.collider.gameObject);
		}
	}

	#endregion
}
