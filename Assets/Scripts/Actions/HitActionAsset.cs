using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitActionAsset : ActionAsset
{
	public override ActionInstance CreateInstance()
	{
		return new HitActionInstance(this);
	}
}