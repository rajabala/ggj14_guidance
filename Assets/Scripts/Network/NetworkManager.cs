using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	private static NetworkManager g_Instance;
	private NetworkManager()
	{
		PhotonNetwork.ConnectUsingSettings("v1.0");
	}

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

	void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		if (PhotonNetwork.otherPlayers.Length > 0) {
			PhotonNetwork.isMessageQueueRunning = false; //Stop the RPC calls when game is loading
			GlobalPlayer.LoadNextLevel();
				}
	}

	void OnPhotonPlayerDisconnected(PhotonPlayer player) { // Network override.
		GlobalPlayer.LoadSceneLoad();
	}

	//Player 1
	//GlobalPlayer.g_PlayerID = GlobalPlayer.EPlayerId.PlayerOne
	//Player 2
	//GlobalPlayer.g_PlayerID = GlobalPlayer.EPlayerId.PlayerTwo;
}
