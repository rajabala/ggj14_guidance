using UnityEngine;
using System.Collections;

public static class GlobalPlayer {
	public enum EPlayerId {PlayerOne = 0, PlayerTwo = 1, None = -1};

	public static string[] g_LevelNames = {"Scene_Load", "Scene_Lvl1", "Scene_Lvl2", "Scene_Lvl3", "Scene_Lvl4",
										   "Scene_Lvl5", "Scene_Lvl6", "Scene_Lvl7", "Scene_End"};

	public static EPlayerId g_PlayerID  = EPlayerId.None;
	public static int currentLevel      = 0;

	public static void LoadNextLevel() {
		// don't destroy the network manager component
		GameObject go = GameObject.Find("NetworkManager");
		if (go == null)
			Debug.Log("Couldn't find network manager");
		else 
			GameObject.DontDestroyOnLoad(go);

		Application.LoadLevel(g_LevelNames[(++currentLevel) % g_LevelNames.Length]);
	}

	public static void LoadSceneLoad() {
		Application.LoadLevel(g_LevelNames[0]);
	}

	public static void PrintDetails()
	{
		Debug.Log ("Player id = " + g_PlayerID);
	}
}
