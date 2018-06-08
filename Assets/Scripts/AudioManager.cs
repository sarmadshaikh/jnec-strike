using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	public LevelManager levelManager;
	public AudioClip[] backgroundMusic;

	AudioSource audioSource;

	static AudioManager instance = null;

	AudioManager getInstance()
	{
		return instance;
	}

	void Awake ()
	{
		if (instance != null)
			Destroy (gameObject);
		else
			instance = this;
		DontDestroyOnLoad (this);

		audioSource = GetComponent <AudioSource> ();
	}

	void Start ()
	{

	}

	void OnLevelWasLoaded ()
	{
		audioSource.Stop ();
		audioSource.clip = backgroundMusic [levelManager.GetCurrentScene ()];
		audioSource.loop = true;
		audioSource.volume = 1.0f;
		audioSource.Play ();
	}
	/*
	void Update () {
		audioSource.Stop ();
		audioSource.clip = backgroundMusic [levelManager.GetCurrentScene ()];
		audioSource.loop = true;
		audioSource.volume = 1.0f;
		audioSource.Play ();
	}*/
}
