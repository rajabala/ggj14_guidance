using UnityEngine;
using System.Collections;

public class SpriteFader : MonoBehaviour {
	public float  fadeTime = 3.0f; // visible for 3 seconds
	private float deltaTime = 0.0f, startTime = 0.0f;
	private SpriteRenderer sr;

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer>();
		if (sr == null)
			Debug.LogError("No sprite renderer for fader script...");
		else 
			sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.0f); // make invisible at start

	}
	
	// Update is called once per frame
	void Update () {
		deltaTime = Time.realtimeSinceStartup - startTime;
		if (deltaTime < fadeTime) {
			sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f - (deltaTime/fadeTime));
		}
	}

	void ShowIcon(Vector3 worldPos) {
		Debug.Log ("SHow icon calle.d,,,");
		startTime = Time.realtimeSinceStartup;
		sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
		transform.position = worldPos;
	}
}
