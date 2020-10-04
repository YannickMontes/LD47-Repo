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

	public static System.Random rng = new System.Random();

	public static void Shuffle<T>(this IList<T> list)
	{
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1);
			T value = list[k];
			list[k] = list[n];
			list[n] = value;
		}
	}
}