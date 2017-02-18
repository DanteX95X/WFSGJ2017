using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	#region variables
	GameObject player;
	Vector3 offset;

	#endregion

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		offset = transform.position - player.transform.position;
	}
	

	void LateUpdate()
	{
		transform.position = player.transform.position + offset;
	}
}
