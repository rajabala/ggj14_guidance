using UnityEngine;
using System.Collections;

public class ProximityBehavior : MonoBehaviour {
	public float fadeLength = 3.0f;
	public Material matMouseReveal;

	private bool _wasInvisible = false;
	private float _fadeStart = -5.0f;
	private Color _revealColor;


	// Use this for initialization
	void Start () {
		// reveal invisible blocks with the same color as the player sees? TODO not a sensible concept :-/
		if (GlobalPlayer.g_PlayerID == GlobalPlayer.EPlayerId.PlayerOne) {
			_revealColor = GlobalPlayer.g_playerBlockColors[0];
		}
		else if (GlobalPlayer.g_PlayerID == GlobalPlayer.EPlayerId.PlayerTwo)
			_revealColor = GlobalPlayer.g_playerBlockColors[1];
	}
	
	// Update is called once per frame
	void Update () {
		if (_wasInvisible && (Time.realtimeSinceStartup - _fadeStart) > fadeLength) {
			renderer.enabled = false;
		}
	}

	void MouseClickCallback(Vector3 worldPos) {
		_fadeStart = Time.realtimeSinceStartup;

		if (renderer.enabled == false) {
			_wasInvisible = true;
			renderer.material = matMouseReveal;
			// if the object isn't visible to the user, only then bother to execute the shader
			Debug.Log ("Invisible object got mouse click callback");

			renderer.material.SetVector("_MouseWorldPos", worldPos);
			renderer.material.SetColor("_RevealColor", _revealColor);
			renderer.enabled = true; // make a portion of it (controlled by shader) visible for `fadeTime`
		}
	}
}
