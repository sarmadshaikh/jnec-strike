using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MyNetworkHUD : MonoBehaviour {

	public LevelManager levelManager;
	public InputField ipAddressInput;
	public Text errorText;

	private NetworkManager networkManager;

	void Start()
	{
		networkManager = GetComponent <NetworkManager> ();
	}

	public void MyStartHost()
	{
		Debug.Log ("Starting Host at: " + Time.timeSinceLevelLoad);
		networkManager.StartHost ();
	}

	public void MyStartClient ()
	{
		Debug.Log ("Starting Client at: " + Time.timeSinceLevelLoad);
		if (ipAddressInput.text.Trim () == "")
			networkManager.networkAddress = "localhost";
		else
			networkManager.networkAddress = ipAddressInput.text;
		
		NetworkClient nc = networkManager.StartClient ();
		if (!nc.isConnected || nc == null) {
			Debug.Log ("Not connected");
			errorText.enabled = true;
		}
	}

	void Update ()
	{
		if (Network.isClient) {
			if (!NetworkServer.active) {
				levelManager.GoToScene (0);
			}
		}
	}
}
