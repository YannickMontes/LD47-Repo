using Yube;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitActionInstance : ActionInstance
{
	public new HitActionAsset Asset { get { return base.Asset as HitActionAsset; } }

	public HitActionInstance(ActionAsset asset) : base(asset)
	{
	}

	public override void Execute(Player player, GameMaster.EDirection keyPressed)
	{
		Vector2 aimPos = (Vector2)player.transform.position + Utils.ConvertDirectionToVector(keyPressed);
		Cell cell = GameMaster.Instance.Grid.GetCell((int)aimPos.x, (int)aimPos.y);
		if (cell != null)
		{
			player.StartCoroutine(SpawnVisual(aimPos));
			for (int i = cell.Entities.Count - 1; i >= 0; i--)
			{
				cell.Entities[i].Hit();
			}
			OnFinishAction(true);
		}
		else
		{
			OnFinishAction(false);
		}
	}

	private IEnumerator SpawnVisual(Vector2 spawnPos)
	{
		GameObject visual = ResourceManager.Instance.AcquireInstance(Asset.VisualToPop, null);
		visual.transform.position = spawnPos;
		yield return new WaitForSeconds(Asset.TimeVisualStay);
		ResourceManager.Instance.ReleaseInstance(visual);
	}
}