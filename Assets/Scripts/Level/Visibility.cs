using UnityEngine;
using System.Collections;

public class Visibility : MonoBehaviour {

	public int whichPlayerCanSeeMe;

	void Start () 
	{
		if (GlobalPlayer.playerID != null)
			SetVisibility();
		else
			print ("Global Player ID is Null! Everything stays visible!");
	}
	

	public void SetVisibility()
	{
		switch (whichPlayerCanSeeMe)
		{
		case 0:
			if (GlobalPlayer.playerID != 0)
				renderer.enabled = false;
			break;
		case 1:
			if (GlobalPlayer.playerID != 1)
				renderer.enabled = false;
			break;
		default:
			renderer.enabled = true;
			break;
		}
			
	}
}
