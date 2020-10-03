using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class MovingEnemy : Hazard
{
	public override void Do()
	{
		transform.position = transform.position + (transform.right * m_casesTravelled);
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player")
		{
			ResourceManager.Instance.ReleaseInstance(this);
		}
	}

	[SerializeField]
	private int m_casesTravelled = 1;
}