using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
	int id;

	[SerializeField]
	List<Material> materials = new List<Material>();

	public int ID
	{
		get { return ID; }
	}

	void Start ()
	{
		id = Random.Range(0, 3);
		transform.GetChild(0).GetComponent<Renderer>().material = materials[id];
	}
	
	void Update ()
	{
		
	}
}
