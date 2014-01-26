using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour, ISwitchable {

	public GameObject targetToActivate;

	private ITriggerable triggerScriptInTarget;

	private bool activated;

	void Start () 
	{
		//Gets the script that has Trigger Method in target object
		triggerScriptInTarget = (ITriggerable)targetToActivate.GetComponent(typeof(ITriggerable));

		activated = false;
	}

	void OnTriggerEnter(Collider player)
	{
		if (!activated)
		{
			activated = true;
			ActivateSwitch();
			print ("Switch Activated");
		}

	}


	public void ActivateSwitch()
	{
		//animation goes here
		triggerScriptInTarget.Trigger();
	}
}
