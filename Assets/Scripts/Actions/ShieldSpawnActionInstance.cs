using Yube;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpawnActionInstance : ActionInstance
{
	public new ShieldSpawnActionAsset Asset { get { return base.Asset as ShieldSpawnActionAsset; } }

	public ShieldSpawnActionInstance(ActionAsset asset) : base(asset)
	{
	}

	public override void Execute(Player player, GameMaster.EDirection keyPressed)
	{
		Vector3 position = player.transform.position + (Vector3)Utils.ConvertDirectionToVector(keyPressed);
		Shield shield = ResourceManager.Instance.AcquireInstance(Asset.ShieldPrefab, null);
		shield.transform.position = position;
	}
}