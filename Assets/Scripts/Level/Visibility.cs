using UnityEngine;
using System.Collections;

public class Visibility : MonoBehaviour {

	public int whichPlayerCanSeeMe;

	void Start () 
	{
		SetVisibility();
	}
	

	public void SetVisibility()
	{
		switch (whichPlayerCanSeeMe)
		{
		case 0:
			if (GlobalPlayer.g_PlayerID == GlobalPlayer.EPlayerId.PlayerOne)
				renderer.enabled = false;
			break;
		case 1:
			if (GlobalPlayer.g_PlayerID == GlobalPlayer.EPlayerId.PlayerTwo)
				renderer.enabled = false;
			break;
		default:
			renderer.enabled = true;
			break;
		}
			
	}
}
