using UnityEngine;
using System.Collections;

public class Visibility : MonoBehaviour {

	public int whichPlayerCanSeeMe;
	private GameObject localPlayer;
	private Player playerStats;


	void Start () 
	{
		localPlayer = GameObject.Find("PlayerOne");
		playerStats = localPlayer.GetComponent<Player>();

		SetVisibility();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetVisibility()
	{
		switch (whichPlayerCanSeeMe)
		{
		case 0:
			if (playerStats.playerID != 0)
				renderer.enabled = false;
			break;
		case 1:
			if (playerStats.playerID != 1)
				renderer.enabled = false;
			break;
		default:
			renderer.enabled = true;
			break;
		}
			
	}
}
