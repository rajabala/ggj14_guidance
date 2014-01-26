using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	public bool xAxis;
	public bool yAxis;
	public bool zAxis;
	public float speed;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (xAxis)
			transform.Rotate ( new Vector3(1,0,0) , speed);

		if (yAxis)
			transform.Rotate ( new Vector3(0,1,0) , speed);

		if (zAxis)
			transform.Rotate ( new Vector3(0,0,1) , speed);
	}
}
