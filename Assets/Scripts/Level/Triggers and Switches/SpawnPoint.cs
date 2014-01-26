using UnityEngine;
using System.Collections;

public class SpawnPoint : MonoBehaviour {

	private LevelController Controller;

	public bool spawnForBothPlayers;

	// Use this for initialization
	void Start () 
	{
		GameObject go = GameObject.Find("BaseLevel");
		Controller = go.GetComponent<LevelController>();
	}
	


	void OnTriggerEnter (Collider player)
	{
		if (player.gameObject.tag == "Player")
		{
			//Set Spawn point in Level Controller to position of the object this script is on
			//Controller.SetSpawnPoint(transform);

			if (spawnForBothPlayers)
			{
				//Gather all the players and tell them to change the spawn point to this one
				GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

				foreach (GameObject person in players)
				{
					Player lets = person.GetComponent<Player>();
					lets.ChangeSpawn(transform);
				}
			}
			else
			{
				Player lets = player.gameObject.GetComponent<Player>();
				lets.ChangeSpawn(transform);
			}
		}
	}


}
