using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour 
{
	

	public GameObject playerOne;
	public GameObject playerTwo;
	private Player playerOneStats;
	private Player playerTwoStats;


	public Transform spawnPoint;



	void Start () 
	{
		GetPlayerIDs();

	}
	

	void Update () 
	{
		
	}

	void GetPlayerIDs()
	{
		playerOneStats = playerOne.GetComponent<Player>();
		playerTwoStats = playerTwo.GetComponent<Player>();

		//Network Code to assign player ID. to be changed later on.
		playerOneStats.playerID = 0;
		playerTwoStats.playerID = 1;
	}
	

}
