using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapActionInstance : ActionInstance
{
	public new SwapActionAsset Asset { get { return base.Asset as SwapActionAsset; } }

	public SwapActionInstance(ActionAsset asset) : base(asset)
	{
	}

	public override void Execute(Player player, GameMaster.EDirection keyPressed)
	{
		Vector2 nextPos = (Vector2)player.transform.position + (Utils.ConvertDirectionToVector(keyPressed) * Asset.Range);
		Cell targetCell = GameMaster.Instance.Grid.GetCell((int)nextPos.x, (int)nextPos.y);
		if (targetCell != null)
		{
			List<GameEntity> toMove = new List<GameEntity>(targetCell.Entities);
			foreach (GameEntity entity in toMove)
			{
				entity.Move(entity.transform.position, player.transform.position);
			}
			player.Move(player.transform.position, nextPos);
			OnFinishAction(true);
		}
		else
		{
			OnFinishAction(false);
		}
	}
}