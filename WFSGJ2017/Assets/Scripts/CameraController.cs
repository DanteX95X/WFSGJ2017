using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	#region variables
	GameObject player;
	Vector3 offset;

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
		offset = transform.position;
	}
	

	void LateUpdate()
	{
		if (player)
			transform.position = player.transform.position + new Vector3(0,0,-10);
	}

	#endregion
}
