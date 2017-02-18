using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Slideshow : MonoBehaviour {
	Image img;
	public float slideDelay = 3;
	Camera mainCamera;
	public string filename="test";
	public int slidesNum=2;
	// Use this for initialization
	void Start () {
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		img = mainCamera.GetComponentInChildren<Image>();
		img.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void showCutscene(string name, int n)
	{
		int num = n;
		Time.timeScale = 0;
		img.enabled = true;

			StartCoroutine(ShowSlide(name,num));
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

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			showCutscene(filename,slidesNum);
		}
	}

}
