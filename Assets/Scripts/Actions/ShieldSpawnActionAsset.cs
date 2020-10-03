using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Shield")]
public class ShieldSpawnActionAsset : ActionAsset
{
	public Shield ShieldPrefab { get { return m_shieldPrefab; } }

	public override ActionInstance CreateInstance()
	{
		return new ShieldSpawnActionInstance(this);
	}

	[SerializeField]
	private Shield m_shieldPrefab = null;
}