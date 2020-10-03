using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class Shield : Hazard
{
	public override void Do()
	{
	}

	protected virtual void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Hazard")
		{
			ResourceManager.Instance.ReleaseInstance(this);
		}
	}
}