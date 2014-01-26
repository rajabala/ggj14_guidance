using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	private static NetworkManager g_Instance;

	// members
    private const string typeName = "RajaServer";
    private const string gameName = "GuideGame";

    public bool isRefreshingHostList;
	private bool haveTwoPlayers = false;
    private HostData[] hostList;

	// singleton instance
	public static NetworkManager Instance{
		get{
			if(g_Instance == null)
			{
				g_Instance = new NetworkManager();
			}
			return g_Instance;
		}
	}

	// Server side logic
	public void StartServer()
	{
		Network.InitializeServer(5, 44000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
		Debug.Log ("Started server, registered with master server for discovery");
	}
	
	void OnServerInitialized() // Network override.
	{
		GlobalPlayer.g_PlayerID = GlobalPlayer.EPlayerId.PlayerOne;
		Debug.Log ("Server initialized");
		GlobalPlayer.PrintDetails();
	}

	void OnPlayerConnected() { // Network override.
		haveTwoPlayers = true;
		Debug.Log ("Player 2 has connected. Loading scene...");
		Application.LoadLevel("Scene_Lvl1");
	}

	void OnPlayerDisconnected() { // Network override.
		haveTwoPlayers = false;
		Debug.Log ("Player 2 has disconnected. Loading loader scene...");	
		DontDestroyOnLoad(this);
		GlobalPlayer.LoadNextLevel();
	}

	// Client side logic
	public void RefreshHostList()
	{
		if (!isRefreshingHostList)
		{
			isRefreshingHostList = true;
			MasterServer.RequestHostList(typeName);
		}
	}

	public void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
		Debug.Log("Attempting to join IP " + hostData.ip[0] + " : " + hostData.port);
	}
	
	void OnConnectedToServer()  // Network override.
	{
		GlobalPlayer.g_PlayerID = GlobalPlayer.EPlayerId.PlayerTwo;
		
		GlobalPlayer.PrintDetails();

		Debug.Log ("Connected to server, loading level...");

		DontDestroyOnLoad(this);
		GlobalPlayer.LoadNextLevel();
	}
	
	public HostData[] GetMasterServerHostList() {
		return MasterServer.PollHostList();
	}
}
