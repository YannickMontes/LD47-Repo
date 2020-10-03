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

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player" || collision.tag == "Hazard")
		{
			ResourceManager.Instance.ReleaseInstance(this);
		}
		else if (collision.tag == "Shield")
		{
			transform.right = transform.right * -1;
			//transform.Rotate(Vector3.forward, 180.0f);
		}
	}

	[SerializeField]
	private int m_casesTravelled = 1;
}