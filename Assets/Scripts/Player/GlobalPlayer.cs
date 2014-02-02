using UnityEngine;
using System.Collections;

public static class GlobalPlayer {
	public enum EPlayerId {PlayerOne = 0, PlayerTwo = 1, None = -1};

	public static string[] g_LevelNames = {"Scene_Load", "Scene_Lvl1", "Scene_Lvl2", "Scene_Lvl3", "Scene_Lvl4",
										   "Scene_Lvl5", "Scene_Lvl6", "Scene_Lvl7", "Scene_End"};

	public static EPlayerId g_PlayerID  = EPlayerId.PlayerTwo; // Set to EPlayerId.None if you want to see all the objects in the world (for local play)
	public static int currentLevel      = 0;
	public static Color[] g_playerBlockColors  = {new Color(120/255, 177/255, 186/255, 255/255), 
												  new Color(118/255, 14/255, 14/255, 255/255)};

	public static void LoadNextLevel() {
		// don't destroy the network manager component
		GameObject nm = GameObject.Find("NetworkManager");
		if (nm == null)
			Debug.Log("Couldn't find network manager");
		else 
			GameObject.DontDestroyOnLoad(nm);

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
