using Yube;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HitActionInstance : ActionInstance
{
	public new HitActionAsset Asset { get { return base.Asset as HitActionAsset; } }

	public HitActionInstance(ActionAsset asset) : base(asset)
	{
	}

	public override void Execute(Player player, GameMaster.EDirection keyPressed)
	{
		Vector2 aimPos = (Vector2)player.transform.position + Utils.ConvertDirectionToVector(keyPressed);
		Cell cell = GameMaster.Instance.Grid.GetCell(aimPos);
		if (cell != null)
		{
			player.StartCoroutine(SpawnVisual(aimPos, keyPressed));
			bool hasHit = false;
			for (int i = cell.Entities.Count - 1; i >= 0; i--)
			{
				hasHit = true;
				cell.Entities[i].Hit();
			}
			OnFinishAction(true, hasHit);
		}
		else
		{
			OnFinishAction(false);
		}
	}

	public override void Reset()
	{
		base.Reset();

		if (m_visualSpawned != null)
		{
			ResourceManager.Instance.ReleaseInstance(m_visualSpawned);
			m_visualSpawned = null;
		}
	}

	private IEnumerator SpawnVisual(Vector2 spawnPos, GameMaster.EDirection direction)
	{
		if (m_visualSpawned != null)
		{
			ResourceManager.Instance.ReleaseInstance(m_visualSpawned);
		}
		m_visualSpawned = ResourceManager.Instance.AcquireInstance(Asset.VisualToPop, null);
		m_visualSpawned.transform.position = spawnPos;
		m_visualSpawned.transform.right = Utils.ConvertDirectionToVector(direction);
		yield return new WaitForSeconds(Asset.TimeVisualStay);
		ResourceManager.Instance.ReleaseInstance(m_visualSpawned);
		m_visualSpawned = null;
	}

	[NonSerialized]
	private GameObject m_visualSpawned = null;
}