using UnityEngine;
using System.Collections;

public class RPCHandler : MonoBehaviour {
	ParticleSystem theSystem;
	public bool localPlayerAtExit = false;
	public bool remotePlayerAtExit = false;
	private float timeElapsed = 0;

	// Use this for initialization
	void Start () {
		theSystem = FindObjectOfType<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update() 
	{
		timeElapsed += Time.deltaTime;

		if (timeElapsed > 0.2f) {
			PlayEffectsServerToClient(transform.position);
			PlayEffectsClientToServer(transform.position);
			timeElapsed = 0.0f;
		}
		MouseEffectsBothWays();

		if (localPlayerAtExit && remotePlayerAtExit) 
			GlobalPlayer.LoadNextLevel();
	}



	// Smoke trail effect RPC
	[RPC] void PlayEffects(Vector3 position)
	{
		theSystem.transform.position = position;
	}

	[RPC] void PlayEffectsServerToClient(Vector3 position)
	{
		if (GlobalPlayer.g_PlayerID == GlobalPlayer.EPlayerId.PlayerOne) {
				}
			//networkView.RPC("PlayEffects", RPCMode.OthersBuffered, transform.position);
	}

	[RPC] void PlayEffectsClientToServer(Vector3 position)
	{
		if (GlobalPlayer.g_PlayerID == GlobalPlayer.EPlayerId.PlayerTwo) {
				}
			//networkView.RPC("PlayEffects", RPCMode.OthersBuffered, transform.position);
	}

	// Mouse click RPC
	[RPC] void MouseEffects(Vector3 worldPos, bool isLeft)
	{
		if (isLeft) {
			//Debug.Log ("Left click at " + worldPos);
			GameObject.Find("FacebookLike").SendMessage("ShowIcon", new Vector3(worldPos.x, worldPos.y, 3));
		} 
		else {
			//Debug.Log ("Right click at " + worldPos);
			GameObject.Find("FacebookDislike").SendMessage("ShowIcon", new Vector3(worldPos.x, worldPos.y, 3));
		}


	}
	
	[RPC] void MouseEffectsBothWays()
	{
		 bool leftMouseDown = Input.GetMouseButtonDown (0) ,
					rightMouseDown= Input.GetMouseButtonDown (1); 

		if (leftMouseDown || rightMouseDown) {
			Plane intersectPlane = new Plane(new Vector3(0,0,-1), new Vector3(0,0, 5));
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float ent = 100.0f;

			if (intersectPlane.Raycast(ray, out ent))
			{
				//Debug.Log("Plane Raycast hit at distance: " + ent);
				Vector3 hitPoint = ray.GetPoint(ent);
				//networkView.RPC ("MouseEffects", RPCMode.OthersBuffered, hitPoint, leftMouseDown);
		     }
		}
	}


	// Portal exit RPC
	[RPC] void ExitStatus(bool otherPlayerExitStatus) {
		remotePlayerAtExit = otherPlayerExitStatus;
	}

	[RPC] void TellExitStatusToOthers() {
		//networkView.RPC ("ExitStatus", RPCMode.OthersBuffered, localPlayerAtExit);
	}
	
	public void PortalExitCallback(bool playerAtExit) 
	{
		// called by the exit trigger
		localPlayerAtExit = playerAtExit;
		TellExitStatusToOthers();
	}


	// Player death RPC
	public void KillPlayerCallback(Vector3 worldPos) {
		//networkView.RPC ("DeathEffects", RPCMode.OthersBuffered, worldPos);
	}

	[RPC] void DeathEffects(Vector3 worldPos) {
		GameObject.Find("DeathSkull").SendMessage("ShowIcon", new Vector3(worldPos.x, worldPos.y, 3));
	}

}
