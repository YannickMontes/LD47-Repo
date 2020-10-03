using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitActionInstance : ActionInstance
{
	public HitActionInstance(ActionAsset asset) : base(asset)
	{
	}

	public override void Execute(Player player, Player.EKeyPressed keyPressed)
	{
		throw new System.NotImplementedException();
	}
}