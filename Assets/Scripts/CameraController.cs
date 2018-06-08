using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	private float maxX = 60f, minX = -60f;

	public float sensitivityX = 5f, sensitivityY = 5f;
	public GameObject player;

	Camera cam;
	float rotationX = 0f, rotationY = 0f;
	Vector3 rotationOffset;


	void Awake ()
	{
		cam = GetComponent<Camera> ();
	}

	// Use this for initialization
	void Start () {		
		Cursor.lockState = CursorLockMode.Locked;
		//rotationOffset = cam.transform.parent.eulerAngles + cam.transform.eulerAngles;
	}
	
	// Update is called once per frame
	void Update () {
		rotationY += Input.GetAxis ("Mouse X") * sensitivityX;
		rotationX += Input.GetAxis ("Mouse Y") * sensitivityY;

		rotationX = Mathf.Clamp (rotationX, minX, maxX);

		player.transform.eulerAngles = new Vector3 (0f, rotationY, 0f);
		cam.transform.eulerAngles = new Vector3 (-rotationX, rotationY, 0f);
		//cam.transform.parent.eulerAngles = cam.transform.eulerAngles + rotationOffset;

		if (Cursor.lockState == CursorLockMode.None) {
			if (Input.GetMouseButton (1)) {
				Cursor.lockState = CursorLockMode.Locked;
			}
		}
		/*
		if (Input.GetKey (KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
		*/
	}
}
