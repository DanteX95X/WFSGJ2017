using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandOfGod : MonoBehaviour {

	#region variables
	GameObject player;

	[SerializeField]
	float speedX;
	#endregion

	#region properties

	public GameObject Player
	{
		get { return player; }
		set
		{
			player = value;
		}
	}

	#endregion

	#region  methods

	void Start()
	{
		player = null;
	}


	void Update()
	{
		if (player)
		{
			Vector2 velocity = new Vector2(speedX, 0);
			if (transform.position.y > player.transform.position.y)
				velocity.y = -1;
			else if (transform.position.y < player.transform.position.y)
				velocity.y = 1;

			GetComponent<Rigidbody2D>().velocity = velocity;
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy(other.gameObject);
		}
	}

	#endregion
}
