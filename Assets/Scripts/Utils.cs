using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
	public static Vector2 ConvertDirectionToVector(GameMaster.EDirection direction)
	{
		switch (direction)
		{
			case GameMaster.EDirection.UP:
				return Vector2.up;

			case GameMaster.EDirection.DOWN:
				return Vector2.down;

			case GameMaster.EDirection.LEFT:
				return Vector2.left;

			case GameMaster.EDirection.RIGHT:
				return Vector2.right;
		}
		return Vector2.zero;
	}
}