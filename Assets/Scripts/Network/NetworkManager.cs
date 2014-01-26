using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	// statics
	private static NetworkManager g_nm;

	// members
	public GameObject playerPrefab;
	public GUIText infoText = "Welcome";

    private const string typeName = "RajaServer";
    private const string gameName = "GuideGame";

    private bool isRefreshingHostList = false;
    private HostData[] hostList;
	
	// Monobehavior overrides
    void OnGUI()
    {
        if (!Network.isClient && !Network.isServer)
        {
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
                StartServer();

            if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
                RefreshHostList();

            if (hostList != null)
            {
                for (int i = 0; i < hostList.Length; i++)
                {
                    if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
                        JoinServer(hostList[i]);
                }
            }
        }
    }

	void Update()
	{
		if (isRefreshingHostList && MasterServer.PollHostList().Length > 0)
		{
			isRefreshingHostList = false;
			hostList = MasterServer.PollHostList();
		}
	}

	// Server side logic
	private void StartServer()
	{
		Network.InitializeServer(5, 44000, !Network.HavePublicAddress());
		MasterServer.RegisterHost(typeName, gameName);
	}
	
	void OnServerInitialized() // Network override.
	{

	}

	void OnPlayerConnected() { // Network override.
		SpawnServerPlayer();
	}

	void OnPlayerDisconnected() { // Network override.

	}

	private void SpawnServerPlayer() 
	{
		Instantiate(playerPrefab, new Vector3(0, 0, 5), Quaternion.identity, 0);
	}

	// Client side logic
	private void RefreshHostList()
	{
		if (!isRefreshingHostList)
		{
			isRefreshingHostList = true;
			MasterServer.RequestHostList(typeName);
		}
	}

	private void JoinServer(HostData hostData)
	{
		Network.Connect(hostData);
	}
	
	void OnConnectedToServer()  // Network override.
	{
		SpawnClientPlayer();
	}
	
	private void SpawnClientPlayer()
	{
		Instantiate(playerPrefab, new Vector3(5, 3, 5), Quaternion.identity, 0);
	}
}
