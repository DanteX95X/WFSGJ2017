using System;
using System.Collections.Generic;
using UnityEngine;

class Game : MonoBehaviour
{
	#region variables

	[SerializeField]
	List<GameObject> checkpoints = null;
	[SerializeField]
	GameObject playerPrefab = null;

	int currentCheckpoint;
	GameObject player;

	#endregion

	#region properties

	#endregion

	#region methods

	void Start()
	{
		currentCheckpoint = 0;
		player = null;
	}


	void Update()
	{
		if(player == null && currentCheckpoint < checkpoints.Count)
		{
			player = Instantiate(playerPrefab, checkpoints[currentCheckpoint].transform.position, checkpoints[currentCheckpoint].transform.rotation) as GameObject;
			GetComponent<CameraController>().Player = player;
		}
	}

	public void NextCheckpoint(GameObject checkpoint)
	{
		for(int i = currentCheckpoint; i < checkpoints.Count; ++i)
		{
			if(checkpoints[i] == checkpoint)
			{
				currentCheckpoint = i;
			}
		}
	}
	#endregion
}
