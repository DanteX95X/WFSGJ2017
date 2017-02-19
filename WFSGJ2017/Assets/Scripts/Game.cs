using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Game : MonoBehaviour
{
	#region variables

	[SerializeField]
	List<GameObject> checkpoints = null;
	[SerializeField]
	GameObject handOfGod = null;
	[SerializeField]
	GameObject playerPrefab = null;
	[SerializeField]
	GameObject levelFragmentParent = null;
	[SerializeField]
	List<GameObject> levelFragments = null;
	[SerializeField]
	GameObject flag = null;

	List<GameObject> lifes = new List<GameObject>();

	[SerializeField]
	int currentCheckpoint = 0;
	GameObject player;

	[SerializeField]
	GameObject collectible = null;

	#endregion

	#region properties

	#endregion

	#region methods

	void Start()
	{
		Slideshow checkpointSlideshow = checkpoints[currentCheckpoint].GetComponent<Slideshow>();
		if(checkpointSlideshow)
            checkpointSlideshow.ShowCutscene();
		Instantiate(flag, checkpoints[currentCheckpoint].transform.position, checkpoints[currentCheckpoint].transform.rotation).transform.Translate(new Vector3(0, 0, 1));

		player = null;

		for(int i = 0; i < 6; ++i)
		{
			GameObject dummy = Instantiate(collectible) as GameObject;
			GainLife(dummy.transform.GetChild(0).gameObject);
			Destroy(dummy);
		}
	}


	void Update()
	{
		if(Input.GetButtonDown("Cancel"))
		{
			Application.LoadLevel(0);
		}

		if(Input.GetKeyDown(KeyCode.Tab))
		{
			if (currentCheckpoint + 1 < checkpoints.Count)
			{
				NextCheckpoint(checkpoints[currentCheckpoint + 1]);
				Destroy(player);
			}
			else
			{
				Debug.Log("You won");
			}
		}

		if(player == null && currentCheckpoint < checkpoints.Count)
		{
			player = Instantiate(playerPrefab, checkpoints[currentCheckpoint].transform.position, checkpoints[currentCheckpoint].transform.rotation) as GameObject;
			if (lifes.Count > 0)
			{
				player.transform.GetChild(0).GetComponent<Renderer>().material = lifes[lifes.Count - 1].GetComponent<Renderer>().material;
				Destroy(lifes[lifes.Count - 1]);
				lifes.RemoveAt(lifes.Count - 1);
			}
			else
			{
				Debug.Log("You lose");
				StartCoroutine("Loose");
			}

			GetComponent<CameraController>().Player = player;
			handOfGod.GetComponent<HandOfGod>().Player = player;
			handOfGod.transform.position = new Vector3(player.transform.position.x - 10, player.transform.position.y, -1);
			for (int i = currentCheckpoint; i < levelFragmentParent.transform.childCount && i < levelFragments.Count; ++i)
			{
				GameObject child = levelFragmentParent.transform.GetChild(i).gameObject;
				GameObject newFragment = Instantiate(levelFragments[i], child.transform.position, child.transform.rotation) as GameObject;
				newFragment.transform.parent = levelFragmentParent.transform;
				Destroy(child);
			}
		}
	}
	IEnumerator Loose()
	{
		Time.timeScale = 0;
		GetComponentsInChildren<Image>()[1].enabled = true;
		yield return new WaitForSecondsRealtime(2);
		GetComponentsInChildren<Image>()[1].enabled = false;
		Time.timeScale = 1;
		//Application.LoadLevel(0);
	}

	public void NextCheckpoint(GameObject checkpoint)
	{
		for(int i = currentCheckpoint + 1; i < checkpoints.Count; ++i)
		{
			if(checkpoints[i] == checkpoint)
			{
				Slideshow checkpointSlideshow = checkpoint.GetComponent<Slideshow>();
                if (checkpointSlideshow)
					checkpointSlideshow.ShowCutscene();

				currentCheckpoint = i;
				Instantiate(flag, checkpoint.transform.position, checkpoint.transform.rotation).transform.Translate(new Vector3(0,0,1));
			}
		}
	}
	public void GainLife(GameObject life)
	{
		life.transform.parent = transform.GetChild(1);
		life.transform.position = transform.GetChild(1).position + new Vector3(lifes.Count, 0, 0);
		lifes.Add(life);
	}
	#endregion
}
