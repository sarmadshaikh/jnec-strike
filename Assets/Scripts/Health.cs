using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class Health : NetworkBehaviour {

	public Slider healthSlider;
	public int maxHealth = 100;

	[SyncVar(hook = "OnCurrentHealthChange")]
	public int currentHealth;

	public AudioClip hurt, die;
	private AudioSource audioSource;

	void Start ()
	{
		currentHealth = maxHealth;
		healthSlider.value = currentHealth;
		audioSource = GetComponent <AudioSource> ();
	}

	public void TakeDamage(int damage)
	{
		if (!isServer)
			return;

		Debug.Log ("Player hit");
		currentHealth -= damage;
		if (currentHealth > 0) {
			audioSource.clip = hurt;
			audioSource.Play ();
		} else {
			Die ();
			Invoke ("Respawn", 3.0f);
		}
	}

	void OnCurrentHealthChange (int currentHealth)
	{
		healthSlider.value = currentHealth;
	}

	void Die()
	{
		audioSource.clip = die;
		audioSource.Play ();
		this.enabled = false;
	}

	void Respawn ()
	{
		this.transform.position = Vector3.zero + new Vector3 (4.0f, 5.0f, 2.0f);
		currentHealth = maxHealth;
		this.enabled = true;
	}
}
