using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	void OnPhotonPlayerDisconnected(PhotonPlayer player)
	{
		Debug.Log("The other player disconnected");
		GlobalPlayer.currentLevel = 0;
		GlobalPlayer.LoadSceneLoad();
	}
}
