using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	#region variables

	[SerializeField]
	float speed = 10;
	[SerializeField]
	Vector3 moveVector = new Vector3(10,0,0);

	Vector3 startPos;
	Vector3 endPos = new Vector3(0,0,0);
	Vector3 temp;
	Vector3 temp2;
	float startTime;
	float journeyLength;

	#endregion

	#region properties

	#endregion

	#region methods

	void Start ()
	{
		startPos = transform.position;
		endPos = startPos + moveVector;
		temp = endPos - startPos; 

		startTime = Time.time;
		journeyLength = Vector3.Distance(startPos, endPos);
	}
	
	
	void Update () 
	{

		if (Mathf.Abs((transform.position - startPos).magnitude) >= Mathf.Abs(temp.magnitude))
		{
			speed = -speed;
			temp2 = startPos;
			startPos = endPos;
			endPos = temp2;

		}
		GetComponent<Rigidbody2D>().velocity = temp.normalized*speed;
	}

	#endregion
}
