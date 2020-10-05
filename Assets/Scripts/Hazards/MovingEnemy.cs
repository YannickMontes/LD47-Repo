using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class MovingEnemy : Hazard
{
	public override void Do()
	{
		Vector2 nextPos = transform.position + (transform.right * m_casesTravelled);
		if (GameMaster.Instance.Grid.GetCell((int)nextPos.x, (int)nextPos.y) == null)
		{
			OnMapBorderReached();
		}
		else
		{
			Move(transform.position, nextPos);
		}
	}

	protected virtual void OnMapBorderReached()
	{
		InvertDirection();
		Vector2 nextPos = transform.position + (transform.right * m_casesTravelled);
		Move(transform.position, nextPos);
	}

	protected override void OnShieldCollide()
	{
		base.OnShieldCollide();
		InvertDirection();
	}

	[SerializeField]
	private int m_casesTravelled = 1;
}