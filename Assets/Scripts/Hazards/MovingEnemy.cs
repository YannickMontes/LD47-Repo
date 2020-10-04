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

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player" || collision.tag == "Hazard")
		{
			ResourceManager.Instance.ReleaseInstance(this);
		}
		else if (collision.tag == "Shield")
		{
			OnShieldContact();
		}
	}

	protected virtual void OnShieldContact()
	{
		InvertDirection();
	}

	protected void InvertDirection()
	{
		transform.right = transform.right * -1;
	}

	[SerializeField]
	private int m_casesTravelled = 1;
}