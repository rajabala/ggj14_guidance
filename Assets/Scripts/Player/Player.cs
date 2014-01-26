using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour, IKillable, ITeleportable {

	public int playerID;

	public Transform spawnPoint;


	void Start () 
	{
		playerID = (int) GlobalPlayer.g_PlayerID;	
		SmoothFollow follow = Camera.main.GetComponent<SmoothFollow> ();
		follow.target = transform;
	}
	

	void Update () 
	{
		
	}
	
	public void Kill()
	{
		print("I'm player"+playerID.ToString()+" and I died");

		transform.position = spawnPoint.position;
	}

	public void Teleport(Transform newLocation)
	{
		transform.position = newLocation.position;
	}

	public void ChangeSpawn(Transform newLocation)
	{

		spawnPoint = newLocation;
		print ("New spawnpoint made for "+playerID.ToString());
	}
}
