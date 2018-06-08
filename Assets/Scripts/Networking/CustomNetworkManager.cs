using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CustomNetworkManager : NetworkDiscovery {

	void Start ()
	{
		InitializeNetworkDiscovery ();
	}

	public bool InitializeNetworkDiscovery ()
	{
		return Initialize ();
	}

	public void SetBroadcastData (string broadCastPayload)
	{
		broadcastData = broadCastPayload;
	}

	public void StartBroadcast ()
	{
		StartAsServer ();
	}

	public void ReceiveBroadcast ()
	{
		StartAsClient ();
	}

	public override void OnReceivedBroadcast (string fromAddress, string data)
	{
		base.OnReceivedBroadcast (fromAddress, data);
	}
		
}
