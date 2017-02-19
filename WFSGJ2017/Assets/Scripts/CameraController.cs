using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	#region variables
	GameObject player;
	GameObject background;
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
	
	void Start ()
	{
		player = null;
		player = GameObject.FindGameObjectWithTag("Player");
		background = GameObject.FindGameObjectWithTag("GameController");
	}
	

	void LateUpdate()
	{
		if (player)
		{
			transform.position = player.transform.position + new Vector3(0, 0, -10);
			background.transform.position = transform.position + new Vector3(0,0,15);
		}
	}

	#endregion
}
