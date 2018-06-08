using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PauseManager : NetworkBehaviour {

	Canvas optionsMenu;
	public GameObject player;
	public GameObject networkManager;
	public GameObject levelManager;

	// Use this for initialization
	void Start () {
		optionsMenu = GetComponent <Canvas> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			player.GetComponent<PlayerController> ().enabled = false;
			optionsMenu.enabled = true;
			//optionsCanvas.enabled = true;
		}

	}

	public void DisableOptionsMenu ()
	{
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		optionsMenu.enabled = false;
		player.GetComponent<PlayerController> ().enabled = true;
		//optionsCanvas.enabled = false;
	}
		
	public void GoBackToMainMenu ()
	{
		if (isClient)
			networkManager.GetComponent <NetworkManager> ().StopClient ();
		else if (isServer)
			networkManager.GetComponent <NetworkManager> ().StopHost ();
		Network.Disconnect ();
		Destroy (networkManager);

		levelManager.GetComponent <LevelManager> ().GoToScene (0);
	}

}
