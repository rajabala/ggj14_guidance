using UnityEngine;
using System.Collections;

public class DestroyOnTrigger : MonoBehaviour, ITriggerable {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Trigger()
	{
		Destroy(gameObject);
	}
}
