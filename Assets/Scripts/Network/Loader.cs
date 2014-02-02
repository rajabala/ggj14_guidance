using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {
	public GUIText gt;
	void Awake() {
	}

	void Start()
	{
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	// Monobehavior overrides
	void OnGUI()
	{
		GUILayout.Label (PhotonNetwork.connectionStateDetailed.ToString ());

		// GUI Reference
		if (GUI.Button (new Rect (100, 100, 250, 100), "Create Room")) {
			string roomName = Random.Range(0, 100000).ToString();
			PhotonNetwork.CreateRoom(roomName);
				}
		if (GUI.Button (new Rect (400, 100, 300, 100), "Join Random Room")) {
			PhotonNetwork.JoinRandomRoom();
				}
		//Snippet for getting the room list
		//foreach (RoomInfo room in PhotonNetwork.GetRoomList())
		//{
		//	GUILayout.Label(room.name + " " + room.playerCount + "/" + room.maxPlayers);
		//}
	}

	void OnPhotonRandomJoinFailed(){
		Debug.Log("Failed to join a random room");
	}

	void OnPhotonCreateGameFailed(){
		Debug.Log("Failed to create a room");
		}

	void OnJoinedRoom()
	{
		if (PhotonNetwork.playerList.Length >= 2) {
			GlobalPlayer.g_PlayerID = GlobalPlayer.EPlayerId.PlayerTwo;
			PhotonNetwork.isMessageQueueRunning = false;
			GlobalPlayer.LoadNextLevel();
				}
	}

	void OnPhotonPlayerConnected(PhotonPlayer player)
	{
		if (PhotonNetwork.otherPlayers.Length > 0) {
			GlobalPlayer.g_PlayerID = GlobalPlayer.EPlayerId.PlayerOne;
			PhotonNetwork.isMessageQueueRunning = false; //Stop the RPC calls when game is loading
			GlobalPlayer.LoadNextLevel();
		}
	}

	void Update()
	{

	}
}
