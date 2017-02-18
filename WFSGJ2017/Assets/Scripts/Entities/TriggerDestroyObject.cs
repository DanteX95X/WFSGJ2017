using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDestroyObject : MonoBehaviour {

	#region variables

	[SerializeField]
	GameObject objectToDestroy;

	#endregion

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			Destroy(objectToDestroy);
		}
	}
}
