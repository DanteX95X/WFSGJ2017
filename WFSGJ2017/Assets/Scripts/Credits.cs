using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour {
	public float writeSpeed = 0.05f;
	public float backToMenuDelay = 4;
	Slideshow show;
	Camera mainCamera;
	Text txt;
	bool finishedWrite;
	bool finishedMov;
	int size;
	// Use this for initialization
	IEnumerator Start()
	{
		show = GetComponent<Slideshow>();
		mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		txt = mainCamera.GetComponentInChildren<Text>();
		yield return new WaitForSeconds(0.5f);
		show.ShowCutscene();
		StartCoroutine("Checker");
		StartCoroutine("Writer");
		finishedWrite = false;
		finishedMov = false;
	
	}
	IEnumerator Checker()
	{
		while (!finishedMov|| !finishedWrite)
		{
			if (mainCamera.GetComponentInChildren<Image>().enabled == false)
			{
				finishedMov = true;
				mainCamera.GetComponentInChildren<Image>().enabled = true;

			}
			yield return new WaitForSecondsRealtime(0.05f);
		}
		yield return new WaitForSecondsRealtime(backToMenuDelay);
		SceneManager.LoadScene("Menu");
	}
	IEnumerator Writer()
	{	
		size = txt.cachedTextGenerator.fontSizeUsedForBestFit;
		txt.fontSize = size;
		txt.text = "";
		txt.color = new Color(0,0,0);
		txt.resizeTextForBestFit = false;
		int i;
		string str = "This game was created during Warsaw Film School Game Jam 2.\r\n\r\nCreated by:\r\nJacek Kozieja (Asterix)\r\nDaniel Lewiński (Dante)\r\nWojciech Płatek (Wowo)\r\nKrzysztof Taperek (Raptor)\r\n\r\nGraphics:\r\nZakłady Przemysłu Cukierniczego \"Otmuchów\" S.A. (mostly)\r\nRaptor as THE HAND\r\n\r\nMusic:\r\n\"I know 3 chords!\" by Asterix\r\n\"Funny Song\" from www.bensound.com\r\n\r\nLots of gummybears were severly harmed in the making of this game.";
		for ( i= 0; i < str.Length; i++)
		{
			txt.text += str[i];
			yield return new WaitForSecondsRealtime(writeSpeed);
		}
		if(i==str.Length)
			finishedWrite = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Cancel"))
		{
			//Application.LoadLevel(0);
			SceneManager.LoadScene("Menu");
		}
	}
}
