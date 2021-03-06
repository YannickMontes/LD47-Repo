﻿using Yube;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Hazard
{
	protected override void Do()
	{
		ShootArrow();
	}

	public void ShootArrow()
	{
		Vector2 spawnPos = transform.position + transform.right;
		if (GameMaster.Instance.Grid.GetCell(spawnPos) != null)
		{
			Arrow arrow = ResourceManager.Instance.AcquireInstance(m_arrowToShoot, null);
			arrow.transform.position = transform.position + transform.right;
			arrow.transform.right = transform.right;
			arrow.Init(m_spawnedDirection);
		}
	}

	[SerializeField]
	private Arrow m_arrowToShoot = null;
	[SerializeField]
	private float m_shotInterval = 2.0f;
}