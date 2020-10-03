using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveActionInstance : ActionInstance
{
	public BasicMoveActionInstance(ActionAsset asset) : base(asset)
	{
	}

	public override void Execute(Player player, Player.EKeyPressed keyPressed)
	{
		Vector2 newPos = player.transform.position;
		switch (keyPressed)
		{
			case Player.EKeyPressed.UP:
				newPos = newPos + Vector2.up;
				break;

			case Player.EKeyPressed.DOWN:
				newPos = newPos + Vector2.down;
				break;

			case Player.EKeyPressed.LEFT:
				newPos = newPos + Vector2.left;
				break;

			case Player.EKeyPressed.RIGHT:
				newPos = newPos + Vector2.right;
				break;
		}
		player.transform.position = newPos;
		OnFinishAction();
	}
}