using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour 
{
	public GameObject localPlayer;
//	private Player playerOneStats;
//	private Player playerTwoStats;	
	public Transform spawnPoint;
    public GameObject facebookLike;
    public GameObject facebookDislike;
    public GameObject facebookLikeOthers;
    public GameObject facebookDislikeOthers;

	void Awake() {
		GlobalPlayer.PrintDetails();
		//GlobalPlayer.g_PlayerID = GlobalPlayer.EPlayerId.PlayerOne;

		if (GlobalPlayer.g_PlayerID == GlobalPlayer.EPlayerId.PlayerOne) {
			GameObject go = GameObject.Find("StartPlayerOne/SpawnPoint"); 
			if (go != null ) spawnPoint = go.transform;
		}
		else if (GlobalPlayer.g_PlayerID == GlobalPlayer.EPlayerId.PlayerTwo) {
			GameObject go = GameObject.Find("StartPlayerTwo/SpawnPoint"); 

			if (go != null ) spawnPoint = go.transform;
		}
		Instantiate(localPlayer, spawnPoint.position, Quaternion.identity);
        Instantiate(facebookLike, spawnPoint.position, Quaternion.identity);
        Instantiate(facebookDislike, spawnPoint.position, Quaternion.identity);
        Instantiate(facebookLikeOthers, spawnPoint.position, Quaternion.identity);
        Instantiate(facebookDislikeOthers, spawnPoint.position, Quaternion.identity);
	}

	void Start(){
		//Level is loaded, resume the message queue
		PhotonNetwork.isMessageQueueRunning = true;
	}
}
