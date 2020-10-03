using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/BasicMove")]
public class BasicMoveActionAsset : ActionAsset
{
	public override ActionInstance CreateInstance()
	{
		return new BasicMoveActionInstance(this);
	}
}