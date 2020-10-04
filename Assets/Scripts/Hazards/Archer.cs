using Yube;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Hazard
{
	public override void Do()
	{
		ShootArrow();
	}

	public void ShootArrow()
	{
		Arrow arrow = ResourceManager.Instance.AcquireInstance(m_arrowToShoot, null);
		arrow.transform.position = transform.position + transform.right;
		arrow.transform.right = transform.right;
		arrow.Init(m_spawnedDirection);
	}

	[SerializeField]
	private Arrow m_arrowToShoot = null;
	[SerializeField]
	private float m_shotInterval = 2.0f;
}