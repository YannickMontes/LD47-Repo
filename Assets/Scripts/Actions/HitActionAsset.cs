using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Actions/Hit")]
public class HitActionAsset : ActionAsset
{
	public GameObject VisualToPop { get { return m_visualToPop; } }
	public float TimeVisualStay { get { return m_timeVisualStay; } }

	public override ActionInstance CreateInstance()
	{
		return new HitActionInstance(this);
	}

	[SerializeField]
	private GameObject m_visualToPop = null;
	[SerializeField]
	private float m_timeVisualStay = 0.5f;
}