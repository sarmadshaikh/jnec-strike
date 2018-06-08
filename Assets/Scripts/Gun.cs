using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Gun : MonoBehaviour {

	public Camera fpsCam;
	public ParticleSystem flash;

	public int damage = 10;
	public float range = 100f;
	public bool zoom = false;

	public int ammoTotal = 56, ammoInMag = 14;
	public Text ammoInMagText, ammoTotalText;


	[HideInInspector] public AudioSource audioSource;

	void Start ()
	{
		audioSource = this.GetComponent <AudioSource> ();
	}

	public void Reload ()
	{
		if (ammoTotal >= 14) {
			ammoTotal -= 14 - ammoInMag;
			ammoInMag = 14;
		} else if (ammoTotal > 0) {
			int deductAmmo = 14 - ammoInMag;
			if (ammoTotal >= deductAmmo) {
				ammoInMag = 14;
				ammoTotal -= deductAmmo;
			} else {
				ammoInMag += ammoTotal;
				ammoTotal = 0;
			}
		} else {
			
		}
	}

	void Update ()
	{
		ammoInMagText.text = ammoInMag.ToString ();
		ammoTotalText.text = ammoTotal.ToString ();
	}

	public void ZoomIn()
	{
		fpsCam.fieldOfView *= 0.5f;
	}

	public void ZoomOut ()
	{
		fpsCam.fieldOfView *= 2f;
	}
}
