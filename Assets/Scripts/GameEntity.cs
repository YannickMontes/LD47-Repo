using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{
	private void OnDisable()
	{
		Cell previousCell = GameMaster.Instance.Grid.GetCell((int)transform.position.x, (int)transform.position.y);
		if (previousCell != null)
		{
			previousCell.RemoveEntity(this);
		}
	}

	public void CheckGridState()
	{
		Cell currentCell = GameMaster.Instance.Grid.GetCell((int)transform.position.x, (int)transform.position.y);
		if (currentCell != null)
		{
			currentCell.AddEntity(this);
		}
	}

	public void Move(Vector2 fromPosition, Vector2 toPosition, bool allowOutsideMaps = false)
	{
		Cell previousCell = GameMaster.Instance.Grid.GetCell((int)fromPosition.x, (int)fromPosition.y);
		if (previousCell != null)
		{
			previousCell.RemoveEntity(this);
		}
		Cell nextCell = GameMaster.Instance.Grid.GetCell((int)toPosition.x, (int)toPosition.y);
		if (nextCell != null || allowOutsideMaps)
		{
			nextCell?.AddEntity(this);
			transform.position = toPosition;
		}
	}
}