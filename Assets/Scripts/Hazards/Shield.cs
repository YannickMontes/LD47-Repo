using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class Shield : Hazard
{
	public override void Do()
	{
	}

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Hazard")
		{
			ScoreManager.Instance.IncreaseScore(m_scorePoint);
			ResourceManager.Instance.ReleaseInstance(this);
		}
	}

	[SerializeField]
	private int m_scorePoint = 5;
}