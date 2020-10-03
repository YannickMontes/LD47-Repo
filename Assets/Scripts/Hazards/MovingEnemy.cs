using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class MovingEnemy : Hazard
{
	public override void Do()
	{
		Move(transform.position, transform.position + (transform.right * m_casesTravelled), true);
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
		transform.right = transform.right * -1;
	}

	[SerializeField]
	private int m_casesTravelled = 1;
	[SerializeField]
	private SpriteRenderer m_spriteRenderer = null;
}