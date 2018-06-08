﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 10f;

	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		rb.velocity = -gameObject.transform.up * speed * Time.deltaTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
