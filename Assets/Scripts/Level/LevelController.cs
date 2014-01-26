using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour 
{
	

	public GameObject localPlayer;
//	private Player playerOneStats;
//	private Player playerTwoStats;


	public Transform spawnPoint;


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
	}
}
