using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public float xRange;
	public float yRange;
	public float cycleTime;	//in seconds
	private float _cycleRatio; //to map cycleTime to [0,1] parameter
	private Vector3 _startPoint;	
	private bool _direction;	//true = towards endpointOne, false = towards endpointTwo
    private float _timeElapsed;
	// Use this for initialization
	void Start () 
	{
		_startPoint = new Vector3 (transform.position.x, transform.position.y, transform.position.z);
		_direction = true;
		_cycleRatio = 0;
        _timeElapsed = 0;
        //0-guard
		if (cycleTime == 0)
			cycleTime = 1;
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
        _timeElapsed += Time.fixedDeltaTime;
		if (_direction) {
            //transform.position = new Vector3(_startPoint.x + xRange * _timeElapsed / cycleTime, _startPoint.y + yRange * _timeElapsed / cycleTime, _startPoint.z);
            rigidbody.velocity = new Vector3 (xRange / cycleTime, yRange / cycleTime, 0);
		} else {
            //transform.position = new Vector3(_startPoint.x + xRange *  (1 - _timeElapsed / cycleTime), _startPoint.y + yRange * (1 - _timeElapsed / cycleTime), _startPoint.z);
            rigidbody.velocity = new Vector3(-xRange / cycleTime, -yRange / cycleTime, 0);
        }
        if(_timeElapsed > cycleTime)
        {
            _direction = !_direction;
            _timeElapsed = 0;
        }
	}

    public Vector3 MovingSpeed
    {
        get {
            if (_direction)
            {
                return new Vector3(xRange / cycleTime, yRange / cycleTime, 0);
            } else
            {
                return new Vector3(-xRange / cycleTime, -yRange / cycleTime, 0);
            }
        }
    }
}
