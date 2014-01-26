using UnityEngine;
using System.Collections;

public class PortalExit : MonoBehaviour {

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			player.BroadcastMessage("PortalExitCallback", true);
		}
	}
	
	void OnTriggerExit(Collider player) 
	{
		if (player.gameObject.tag == "Player")
		{
			player.BroadcastMessage("PortalExitCallback", false);
		}
	}

}
