using UnityEngine;
using System.Collections;

public class Switch : MonoBehaviour, ISwitchable {

	public GameObject targetToActivate;

	public bool twoWay;
	private ITriggerable triggerScriptInTarget;
	private bool activated;
	private float cooldownTime;

	void Start () 
	{
		//Gets the script that has Trigger Method in target object
		triggerScriptInTarget = (ITriggerable)targetToActivate.GetComponent(typeof(ITriggerable));

		activated = false;
		cooldownTime = 1;
	}

	void Update()
	{
		if (activated && twoWay)
		{
			cooldownTime -= Time.deltaTime;
			if (cooldownTime < 0)
				activated = false;
		}
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
