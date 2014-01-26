using UnityEngine;
using System.Collections;

public interface IKillable
{

	void Kill();

}

public interface ITeleportable
{

	void Teleport(Transform newLocation);
}