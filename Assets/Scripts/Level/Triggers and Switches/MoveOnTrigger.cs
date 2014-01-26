using UnityEngine;
using System.Collections;

public class MoveOnTrigger : MonoBehaviour, ITriggerable {

	public float xDirection;
	public float yDirection;
	public float zDirection;
	public float lerpSpeed;	//range [0,1]

	private Vector3 originalLocation;
	private Vector3 moveLocation;

	public bool moveBackAndForth;
	private int moveDirection;	//1 - towards moveLocation, 0 - towards originalLocation

	private bool moving;


	// Use this for initialization
	void Start () 
	{
		moving = false;
		moveDirection = 0;

		if (lerpSpeed > 1)
			lerpSpeed = 1;


		originalLocation = new Vector3 (transform.position.x, 
		                                transform.position.y, 
		                                transform.position.z);

		moveLocation = new Vector3 (transform.position.x + xDirection,
		                            transform.position.y + yDirection, 
		                            transform.position.z + zDirection);
	}

	void Update () 
	{
		if (moving)
		{
			MoveToPosition();
		}

	}

	public void Trigger()
	{
		moving = true;
		print ("Move Trigger Activated");
		if (moveBackAndForth)
			moveDirection = moveDirection == 0 ? 1 : 0; //Switch directions each time this is triggered
		else
			moveDirection = 1;	//One way trigger
	}

	void MoveToPosition()
	{
		Vector3 placeToMoveTo = moveDirection == 0 ? originalLocation : moveLocation;


		transform.position = Vector3.Lerp(transform.position , placeToMoveTo, lerpSpeed);

		if (transform.position == placeToMoveTo)
			moving = false;
	}

}
