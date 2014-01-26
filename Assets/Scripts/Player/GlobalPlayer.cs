using UnityEngine;
using System.Collections;

public static class GlobalPlayer {
	public enum EPlayerId {PlayerOne = 0, PlayerTwo = 1, None = -1};

	public static EPlayerId g_PlayerID = EPlayerId.None;
	public static int currentLevel     = 0;
}
