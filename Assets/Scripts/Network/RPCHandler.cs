using UnityEngine;
using System.Collections;

public class RPCHandler : MonoBehaviour {
	ParticleSystem theSystem;
	float timeElapsed = 0;
	// Use this for initialization
	void Start () {
		theSystem = FindObjectOfType<ParticleSystem> ();
	}
	
	// Update is called once per frame
//	void Update () {
//		timeElapsed += Time.deltaTime;
//		if (timeElapsed > 0.2f) {
//						PlayEffects (transform.position);
//			timeElapsed = 0;
//				}
//	}

	void Update() 
	{
		timeElapsed += Time.deltaTime;

		if (timeElapsed > 0.2f) {
			PlayEffectsServerToClient(transform.position);
			PlayEffectsClientToServer(transform.position);

			timeElapsed = 0.0f;
		}

	}


	[RPC] void PlayEffects(Vector3 position)
	{
		theSystem.transform.position = position;
	}

	[RPC] void PlayEffectsServerToClient(Vector3 position)
	{
		if (GlobalPlayer.g_PlayerID == GlobalPlayer.EPlayerId.PlayerOne)
			networkView.RPC("PlayEffects", RPCMode.OthersBuffered, transform.position);
	}

	[RPC] void PlayEffectsClientToServer(Vector3 position)
	{
		if (GlobalPlayer.g_PlayerID == GlobalPlayer.EPlayerId.PlayerTwo)
			networkView.RPC("PlayEffects", RPCMode.OthersBuffered, transform.position);
	}

//	[RPC] void PlayEffects(Vector3 position)
//	{
//		if (GlobalPlayer.g_PlayerID == GlobalPlayer.EPlayerId.PlayerTwo)
//			networkView.RPC("PlayEffects", RPCMode.OthersBuffered, transform.position);
//		else
//			theSystem.transform.position = position;
//
//
////		if (networkView.isMine)
////			networkView.RPC("PlayEffects", RPCMode.OthersBuffered, transform.position);
////		else
////		{
////			theSystem.transform.position = position;
////		}
//	}
}
