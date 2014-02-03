using UnityEngine;
using System.Collections;

public class RPCHandler : MonoBehaviour {
	ParticleSystem theSystem;
	public bool localPlayerAtExit = false;
	public bool remotePlayerAtExit = false;
	public float mouseRadius  = 3.0f;
	private float timeElapsed = 0;
	private PhotonView _photonView;

	// Use this for initialization
	void Start () {
		theSystem = FindObjectOfType<ParticleSystem> ();
		_photonView = GetComponent<PhotonView> ();
	}
	
	// Update is called once per frame
	void Update() 
	{
		timeElapsed += Time.deltaTime;

		if (timeElapsed > 0.2f) {
			PlayEffectsToTheOther(transform.position);
			timeElapsed = 0.0f;
		}
		MouseEffectsBothWays();

		if (localPlayerAtExit && remotePlayerAtExit) 
			GlobalPlayer.LoadNextLevel();
	}


	//This doesn't work somehow
//	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//	{
//		if (stream.isWriting) {
//						stream.SendNext (transform.position);
//				} else {
//			theSystem.transform.position = (Vector3)stream.ReceiveNext();
//				}
//	}

	
	// Smoke trail effect RPC
	[RPC] void PlayEffects(Vector3 position)
	{
		theSystem.transform.position = position;
	}

	void PlayEffectsToTheOther(Vector3 position)
	{
		_photonView.RPC("PlayEffects", PhotonTargets.OthersBuffered, transform.position);
	}

	// Mouse click RPC
	[RPC] void MouseEffects(Vector3 worldPos, bool isLeft)
	{
		// Game jam effect - display Facebook like/dislike
		if (isLeft) {
			//Debug.Log ("Left click at " + worldPos);
		    //GameObject.Find("FacebookLike").SendMessage("ShowIcon", new Vector3(worldPos.x, worldPos.y, 3));
			GameObject.Find("FacebookLike(Clone)").SendMessage("ShowIcon", new Vector3(worldPos.x, worldPos.y, 3));

			// Let's make it cooler! If mouse click is near an object that is invisible to the player,
			// reveal a small radius around it
			Collider[] hitColliders = Physics.OverlapSphere(worldPos, mouseRadius);
			int i = 0;
			while (i < hitColliders.Length) {
				ProximityBehavior pb = hitColliders[i].GetComponent<ProximityBehavior>();
				if (pb != null)
					pb.SendMessage("MouseClickCallback", worldPos);
				
				i++;
			}
		} 
		else {
			//Debug.Log ("Right click at " + worldPos);
			GameObject.Find("FacebookDislike(Clone)").SendMessage("ShowIcon", new Vector3(worldPos.x, worldPos.y, 3));
		}
	}
	
	void MouseEffectsBothWays()
	{
		// Right click doesn't bode well with browsers. Make it Ctrl + leftclick
		bool leftMouseDown = Input.GetMouseButtonDown (0); 

		if (leftMouseDown) {
			bool isRightMouse = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
			Debug.Log (isRightMouse);

			Plane intersectPlane = new Plane(new Vector3(0,0,-1), new Vector3(0,0, 5));
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float ent = 100.0f;

			if (intersectPlane.Raycast(ray, out ent))
			{
				//Debug.Log("Plane Raycast hit at distance: " + ent);
				Vector3 hitPoint = ray.GetPoint(ent);
				if(!isRightMouse)
				{
					GameObject.Find("FacebookLikeOthers(Clone)").SendMessage("ShowIcon", new Vector3(hitPoint.x, hitPoint.y, 3));
                }
                else
                {
                    GameObject.Find("FacebookDislikeOthers(Clone)").SendMessage("ShowIcon", new Vector3(hitPoint.x, hitPoint.y, 3));
                }
				_photonView.RPC ("MouseEffects", PhotonTargets.OthersBuffered, hitPoint, !isRightMouse);
		     }
		}
	}


	// Portal exit RPC
	[RPC] void ExitStatus(bool otherPlayerExitStatus) {
		remotePlayerAtExit = otherPlayerExitStatus;
	}

	[RPC] void TellExitStatusToOthers() {
		_photonView.RPC ("ExitStatus", PhotonTargets.OthersBuffered, localPlayerAtExit);
	}
	
	public void PortalExitCallback(bool playerAtExit) 
	{
		// called by the exit trigger
		localPlayerAtExit = playerAtExit;
		TellExitStatusToOthers();
	}


	// Player death RPC
	public void KillPlayerCallback(Vector3 worldPos) {
		_photonView.RPC ("DeathEffects", PhotonTargets.OthersBuffered, worldPos);
	}

	[RPC] void DeathEffects(Vector3 worldPos) {
		GameObject.Find("DeathSkull").SendMessage("ShowIcon", new Vector3(worldPos.x, worldPos.y, 3));
	}

}
