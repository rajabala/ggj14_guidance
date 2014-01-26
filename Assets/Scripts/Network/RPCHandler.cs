using UnityEngine;
using System.Collections;

public class RPCHandler : MonoBehaviour {
	ParticleSystem theSystem;
	public GUIText good, bad;

	float timeElapsed = 0;
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

	}

	// Smoke trail effect RPC
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

	// Mouse click RPC
	[RPC] void MouseEffects(Vector3 worldPos, bool isLeft)
	{
		if (isLeft) {
			Debug.Log ("Left click at " + worldPos);
			GameObject.Find("FacebookLike").transform.position = worldPos;
		} 
		else {
			Debug.Log ("Right click at " + worldPos);
			GameObject.Find("FacebookLike").transform.position = worldPos;
		}


	}
	
	[RPC] void MouseEffectsBothWays()
	{
		 bool leftMouseDown = Input.GetMouseButtonDown (0) ,
					rightMouseDown= Input.GetMouseButtonDown (1); 

		if (leftMouseDown || rightMouseDown) {
//				Vector3 screenPos = Input.mousePosition; 
//			    screenPos.z = 0;
//				Vector3 worldPos = Camera.main.ScreenToWorldPoint (screenPos);
//				worldPos.z = -1.75f;

			Plane intersectPlane = new Plane(new Vector3(0,0,-1), new Vector3(0,0, 5));
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float ent = 100.0f;
			if (intersectPlane.Raycast(ray, out ent))
			{
				Debug.Log("Plane Raycast hit at distance: " + ent);
				Vector3 hitPoint = ray.GetPoint(ent);
				networkView.RPC ("MouseEffects", RPCMode.OthersBuffered, hitPoint, leftMouseDown);
		     }
		}
	}



}
