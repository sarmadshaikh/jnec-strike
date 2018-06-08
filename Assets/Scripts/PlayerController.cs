using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public float walkSpeed;
	public float runSpeed;
	public float jumpHeight;
	public Gun gun;

	Animator anim;

	private float speed;
	private bool isGrounded = true;
	private bool crouched = false;
	private bool running = false;
	private bool isDead = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent <Animator> ();
		speed = walkSpeed;
	}
	
	// Update is called once per frame
	void Update () {

		if (!isLocalPlayer || isDead)
			return;

		float x = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
		float z = Input.GetAxis ("Vertical") * speed * Time.deltaTime;
		transform.Translate (x, 0f, z);

		#region HandleInputs
		if (Input.GetButtonDown ("Fire1")) {
			if (Cursor.lockState == CursorLockMode.Locked && gun.ammoInMag > 0)
				Shoot ();
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			gun.Reload ();
		}

		if (Input.GetMouseButtonDown (1)) {
			if (!gun.zoom) {
				gun.ZoomIn ();
			} else {
				gun.ZoomOut ();
			}
			gun.zoom = !gun.zoom;
		}

		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			running = true;
			speed = runSpeed;
		} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			running = false;
			speed = walkSpeed;
		}

		if (Input.GetButtonDown ("Jump")) {
			if (isGrounded)
				Jump ();
			isGrounded = !isGrounded;
		}

		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			if (!crouched)
				Crouch ();
			else
				StandUp ();
			
			crouched = !crouched;
		}
		#endregion
	}

	void Jump()
	{
		//anim.SetTrigger ("Jumped");
		transform.position = Vector3.Lerp (transform.position, transform.position + new Vector3 (0f, jumpHeight, 0f), 1.0f);
		isGrounded = false;
	}

	void Crouch ()
	{
		anim.SetBool ("IsCrouching", true);
		// anim.GetBoneTransform (HumanBodyBones.Hips).transform.Translate (new Vector3 (0f, -2f, 0f));
	}

	void StandUp ()
	{
		anim.SetBool ("IsCrouching", false);
	}

	public override void OnStartLocalPlayer ()
	{
		GetComponentInChildren <Camera> ().enabled = true;
	}
		
	public void Shoot()
	{
		gun.audioSource.Play ();
		gun.flash.Play ();
		gun.ammoInMag -= 1;
		MyShoot ();
		gun.damage = 10;
	}

	//[Command]
	public void MyShoot ()
	{
		RaycastHit hit;
		if (Physics.Raycast (gun.fpsCam.transform.position, gun.fpsCam.transform.forward, out hit, gun.range)) {
			Debug.Log (hit.transform.name);
			if (hit.transform.name == "Body")
				gun.damage = 100;
			Health health = hit.transform.GetComponent<Health> ();
			if (health != null) {
				Debug.LogError ("Player: " + hit.transform.name);
				health.TakeDamage (gun.damage);
			}
			else
				Debug.Log ("Inanimate object");
		}
	}

}
