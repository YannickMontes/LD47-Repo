using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class MovingEnemy : Hazard
{
	protected override void Do()
	{
		Vector2 nextPos = transform.position + (transform.right * m_casesTravelled);
		if (GameMaster.Instance.Grid.GetCell(nextPos) == null)
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

	public override void Move(Vector2 fromPosition, Vector2 toPosition)
	{
		if (!gameObject.activeSelf)
			return;

		Cell nextCell = GameMaster.Instance.Grid.GetCell(toPosition);
		if (nextCell != null)
		{
			bool canMove = true;
			for (int i = nextCell.Entities.Count - 1; i >= 0; i--)
			{
				if (nextCell.Entities[i] is MovingEnemy && this is MovingEnemy && nextCell.Entities[i].transform.right == (transform.right * -1))
				{
					canMove = false;
					ResourceManager.Instance.ReleaseInstance(nextCell.Entities[i]);
				}
			}
			if (!canMove)
			{
				ResourceManager.Instance.ReleaseInstance(this);
			}
			else
			{
				base.Move(fromPosition, toPosition);
			}
		}
	}

	[SerializeField]
	private int m_casesTravelled = 1;
}