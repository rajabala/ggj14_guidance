using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour //, ISwitchable
{

	public Transform exitNode;

	

	void Start () {
	
	}
	

	void Update () {
	
	}

	void OnTriggerEnter(Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			Player lets = player.gameObject.GetComponent<Player>();
			lets.Teleport(exitNode);
		}
	}
}
