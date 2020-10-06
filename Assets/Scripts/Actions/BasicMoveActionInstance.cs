using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveActionInstance : ActionInstance
{
	public BasicMoveActionInstance(ActionAsset asset) : base(asset)
	{
	}

	public override void Execute(Player player, GameMaster.EDirection keyPressed)
	{
		Vector2 newPos = player.transform.position;
		newPos = newPos + Utils.ConvertDirectionToVector(keyPressed);
		if (GameMaster.Instance.CanPlayerMove(newPos))
		{
			player.Move(player.transform.position, newPos);
			OnFinishAction(true);
		}
		else
		{
			OnFinishAction(false);
		}
	}
}