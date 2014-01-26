using UnityEngine;
using System.Collections;

public static class GlobalPlayer {
	public enum EPlayerId {PlayerOne = 0, PlayerTwo = 1, None = -1};

	public static string[] g_LevelNames = {"Scene_Load", "Scene_Lvl1"};
	public static EPlayerId g_PlayerID  = EPlayerId.None;
	public static int currentLevel      = 0;

	public static void LoadNextLevel() {
		Application.LoadLevel(g_LevelNames[(++currentLevel) % g_LevelNames.Length]);
	}

	public static void PrintDetails()
	{
		Debug.Log ("Player id = " + g_PlayerID);
	}
}
