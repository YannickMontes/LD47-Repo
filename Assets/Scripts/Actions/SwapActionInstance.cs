using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class SwapActionInstance : ActionInstance
{
	public new SwapActionAsset Asset { get { return base.Asset as SwapActionAsset; } }

	public SwapActionInstance(ActionAsset asset) : base(asset)
	{
	}

	public override void Execute(Player player, GameMaster.EDirection direction)
	{
		//BasicSwap(player, direction);
		LineSwap(player, direction);
	}

	private void BasicSwap(Player player, GameMaster.EDirection direction)
	{
		Vector2 nextPos = (Vector2)player.transform.position + (Utils.ConvertDirectionToVector(direction) * Asset.Range);
		Cell targetCell = GameMaster.Instance.Grid.GetCell((int)nextPos.x, (int)nextPos.y);
		if (targetCell != null)
		{
			List<GameEntity> toMove = new List<GameEntity>(targetCell.Entities);
			bool hasSwap = false;
			foreach (GameEntity entity in toMove)
			{
				entity.Move(entity.transform.position, player.transform.position);
				hasSwap = true;
			}
			player.Move(player.transform.position, nextPos);
			OnFinishAction(true, hasSwap);
		}
		else
		{
			OnFinishAction(false);
		}
	}

	private void LineSwap(Player player, GameMaster.EDirection direction)
	{
		Vector2 directionVec = Utils.ConvertDirectionToVector(direction);
		Vector2 nextPos = (Vector2)player.transform.position + directionVec;
		Cell targetCell = GameMaster.Instance.Grid.GetCell((int)nextPos.x, (int)nextPos.y);
		if (targetCell != null)
		{
			bool foundCell = false;
			while (!foundCell)
			{
				if (targetCell.Entities.Count > 0)
				{
					foundCell = true;
				}
				else
				{
					nextPos = nextPos + directionVec;
					Cell nextCell = GameMaster.Instance.Grid.GetCell((int)nextPos.x, (int)nextPos.y);
					if (nextCell == null)
					{
						nextPos -= directionVec;
						foundCell = true;
					}
					else
					{
						targetCell = nextCell;
					}
				}
			}

			List<GameEntity> toMove = new List<GameEntity>(targetCell.Entities);
			bool hasSwap = false;
			foreach (GameEntity entity in toMove)
			{
				entity.Move(entity.transform.position, player.transform.position);
				hasSwap = true;
			}
			player.Move(player.transform.position, nextPos);
			OnFinishAction(true, hasSwap);
		}
		else
		{
			OnFinishAction(false);
		}
	}
}