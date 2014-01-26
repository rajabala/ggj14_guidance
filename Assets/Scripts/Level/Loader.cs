using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {
	private HostData[] hostList; 
	private NetworkManager nm = null;
	public GUIText gt;

	void Awake() {
		nm = NetworkManager.Instance;
	}

	// Monobehavior overrides
	void OnGUI()
	{
		if (!Network.isClient && !Network.isServer)
		{
			if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server")) {
				nm.StartServer();
				gt.text = "Waiting for a friend to join...";
			}
			
			if (GUI.Button(new Rect(100, 250, 250, 100), "Refresh Hosts"))
				nm.RefreshHostList();
			
			if (hostList != null)
			{
				for (int i = 0; i < hostList.Length; i++)
				{
					if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
						nm.JoinServer(hostList[i]);
				}
			}
		}
	}
	
	void Update()
	{
		if (nm.isRefreshingHostList && nm.GetMasterServerHostList().Length > 0)
		{
			nm.isRefreshingHostList = false;
			hostList = nm.GetMasterServerHostList();
		}
	}
}
