using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
	int currentCheckpoint = -1;
	GameObject player = null;


	#endregion

	#region properties

	#endregion

	#region methods

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
	}


	void Update()
	{
		if(Input.GetButtonDown("Cancel"))
		{
			//Application.LoadLevel(0);
			Time.timeScale = 1; 
			SceneManager.LoadScene("Menu");
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
			if (currentCheckpoint < 0)
				currentCheckpoint = 0;

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
		//Application.LoadLevel("Menu");
		SceneManager.LoadScene("Menu");
	}

	IEnumerator BackToMenu()
	{
		yield return new WaitForSeconds(3);
		//Application.LoadLevel("Menu");
		Time.timeScale = 1; 
		SceneManager.LoadScene("Menu");
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

				if (i == checkpoints.Count - 1)
					StartCoroutine(BackToMenu());
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
