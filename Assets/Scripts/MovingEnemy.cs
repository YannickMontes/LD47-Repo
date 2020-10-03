using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : Enemy
{
	public override void Do()
	{
		transform.position = transform.position + (transform.right * m_casesTravelled);
	}

	[SerializeField]
	private int m_casesTravelled = 1;
}