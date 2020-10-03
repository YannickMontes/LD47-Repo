using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Swap")]
public class SwapActionAsset : ActionAsset
{
	public int Range { get { return m_range; } }

	public SwapActionAsset()
	{
	}

	public override ActionInstance CreateInstance()
	{
		return new SwapActionInstance(this);
	}

	[SerializeField]
	private int m_range = 1;
}