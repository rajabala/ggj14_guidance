using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {
	private NetworkManager nm = null;
	public GUIText gt;
	void Awake() {
		nm = NetworkManager.Instance;
	}

	// Monobehavior overrides
	void OnGUI()
	{
		// GUI Reference
		//if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server"))
		//if (GUI.Button(new Rect(400, 100 + (110 * i), 300, 100), hostList[i].gameName))
	}
	
	void Update()
	{
	}
}
