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
	[SerializeField]
	GameObject levelFragmentParent = null;
	[SerializeField]
	List<GameObject> levelFragments = null;

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
		if(Input.GetButtonDown("Cancel")){
			Application.LoadLevel(0);
		}

		if(player == null && currentCheckpoint < checkpoints.Count)
		{
			player = Instantiate(playerPrefab, checkpoints[currentCheckpoint].transform.position, checkpoints[currentCheckpoint].transform.rotation) as GameObject;
			GetComponent<CameraController>().Player = player;
			for(int i = currentCheckpoint; i < levelFragmentParent.transform.childCount && i < levelFragments.Count; ++i)
			{
				GameObject child = levelFragmentParent.transform.GetChild(i).gameObject;
				GameObject newFragment = Instantiate(levelFragments[i], child.transform.position, child.transform.rotation) as GameObject;
				newFragment.transform.parent = levelFragmentParent.transform;
				Destroy(child);
			}
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
