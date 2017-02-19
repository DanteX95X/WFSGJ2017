using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slideshow : MonoBehaviour
{
	Image img;
	[SerializeField]
	public float slideDelay = 3;
	Camera mainCamera;

	[SerializeField]
	string filename="test";
	[SerializeField]
	int slidesNum=2;

	void Start ()
	{
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		img = mainCamera.GetComponentInChildren<Image>();
		img.enabled = false;
	}
	
	void Update ()
	{
		
	}

	public void ShowCutscene()
	{
		Time.timeScale = 0;
		img.enabled = true;

			StartCoroutine(ShowSlide(filename, slidesNum));
	}

	IEnumerator ShowSlide(string name, int num)
	{
		for (int i = 0; i < num; i++)
		{
			Debug.Log(name + i.ToString());
			img.sprite = Resources.Load<Sprite>(name + i.ToString());
			yield return new WaitForSecondsRealtime(slideDelay);
		}
		img.enabled = false;
		Time.timeScale = 1; 
	}
}
