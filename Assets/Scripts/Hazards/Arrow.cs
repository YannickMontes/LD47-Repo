using Yube;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MovingEnemy
{
	protected override void OnMapBorderReached()
	{
		ResourceManager.Instance.ReleaseInstance(this);
	}
}