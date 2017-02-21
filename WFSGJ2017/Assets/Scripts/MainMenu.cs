using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void StartGame()
	{
		//Application.LoadLevel(Application.loadedLevel+1);
		SceneManager.LoadScene("Level1");
	}
	public void ExitGame()
	{
		Application.Quit();
	}
	public void ShowCredits()
	{
		//Application.LoadLevel(Application.loadedLevel+1);
		SceneManager.LoadScene("Credits");
	}
}
