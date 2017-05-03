using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.IO;

public class CustomNetworkManager : NetworkManager {
	public bool ShouldBeServer;

	public GameObject vrPlayerPrefab;
	private int playerCount = 0;

	public override void OnServerAddPlayer (NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		SpawnMessage message = new SpawnMessage ();
		message.Deserialize (extraMessageReader);

		bool isVrPlayer = message.isVrPlayer;

		Transform spawnPoint = this.startPositions [playerCount];

		GameObject newPlayer;
		if (isVrPlayer) {
			newPlayer = (GameObject)Instantiate (this.vrPlayerPrefab, spawnPoint.position, spawnPoint.rotation);
		} else {
			newPlayer = (GameObject)Instantiate (this.playerPrefab, spawnPoint.position, spawnPoint.rotation);
		}
		NetworkServer.AddPlayerForConnection (conn, newPlayer, playerControllerId);
		playerCount++;
	}

	public override void OnServerRemovePlayer (NetworkConnection conn, UnityEngine.Networking.PlayerController player)
	{
		base.OnServerRemovePlayer (conn, player);
		playerCount--;
	}

	void Start () {
		var settingsPath = Application.dataPath + "/settings.cfg";
        if (File.Exists (settingsPath)) {
			StreamReader textReader = new StreamReader (settingsPath, System.Text.Encoding.ASCII);
			ShouldBeServer = textReader.ReadLine () == "Server";
            networkAddress = textReader.ReadLine();
            textReader.Close ();
		}
		
		Debug.Log ("Starting Network");
		if (ShouldBeServer)
		{
			StartHost ();
		}
		else
		{
			StartClient ();
		}
	}

	public override void OnClientConnect (NetworkConnection conn)
	{
		SpawnMessage extraMessage = new SpawnMessage ();
		extraMessage.isVrPlayer = UnityEngine.VR.VRSettings.enabled;
		ClientScene.AddPlayer (client.connection, 0, extraMessage);
	}
}


