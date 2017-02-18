﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	public float speed = 10;
	public Vector3 moveVector = new Vector3(10,0,0);
	Vector3 startPos;
	Vector3 endPos = new Vector3(0,0,0);
	Vector3 temp;
	Vector3 temp2;
	float startTime;
	float journeyLength;
	// Use this for initialization
	void Start () {
		startPos = transform.position;
		endPos = startPos + moveVector;
		temp = endPos - startPos; 

		startTime = Time.time;
		journeyLength = Vector3.Distance(startPos, endPos);
	}
	
	// Update is called once per frame
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

}
