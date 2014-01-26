using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public float xRange;
	public float yRange;
	public float cycleTime;	//in seconds

	private float cycleRatio; //to map cycleTime to [0,1] parameter

	private Vector3 startPoint;
	
	private bool direction;	//true = towards endpointOne, false = towards endpointTwo

	// Use this for initialization
	void Start () 
	{
		startPoint = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		//endpointTwo = new Vector3 (transform.position.x + xRange, transform.position.y + yRange, transform.position.z);

		direction = true;
		cycleRatio = 0;

		if (cycleTime == 0)
			cycleTime = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (direction)
		{
			cycleRatio += Time.deltaTime/cycleTime;

			if(cycleRatio >= 1)
				direction = false;
		}
		else 
		{
			cycleRatio -= Time.deltaTime/cycleTime;

			if (cycleRatio <=0)
				direction = true;
		}

		//Move Platform
		transform.position = new Vector3 (startPoint.x + (xRange*cycleRatio),
		                                  startPoint.y + (yRange*cycleRatio), 
		                                  startPoint.z);

	}


}
