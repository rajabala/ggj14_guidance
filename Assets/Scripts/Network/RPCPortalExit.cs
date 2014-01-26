using UnityEngine;
using System.Collections;

public class RPCPortalExit : MonoBehaviour {
	public bool localPlayerAtExit = false;
	public bool remotePlayerAtExit = false;

	void Start() {

	}

	void Update()
	{
		if (localPlayerAtExit && remotePlayerAtExit) 
			GlobalPlayer.LoadNextLevel();
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			localPlayerAtExit = true;
			// tell other player you've reached exit via RPC
			TellExitStatusToOthers();
		}
	}
	
	void OnTriggerExit(Collider player) 
	{
		if (player.gameObject.tag == "Player")
		{
			localPlayerAtExit = false;
			// tell other player you're not near the exit anymore via RPC
			TellExitStatusToOthers();
		}
	}

	[RPC] void ExitStatus(bool otherPlayerExitStatus) {
		remotePlayerAtExit = otherPlayerExitStatus;
	}
	

	[RPC] void TellExitStatusToOthers() {
		networkView.RPC ("ExitStatus", RPCMode.OthersBuffered, localPlayerAtExit);
	}
}
