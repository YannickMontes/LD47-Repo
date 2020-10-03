using Yube;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : GameEntity
{
	public IReadOnlyList<GameMaster.EDirection> AllowedDirections { get { return m_allowedDirections; } }

	private void Update()
	{
		m_elapsedTime += Time.deltaTime;
		if (m_elapsedTime >= (1.0f / m_ticMultiplicator))
		{
			Do();
			m_elapsedTime = 0.0f;
		}
	}

	public abstract void Do();

	[SerializeField]
	private float m_ticMultiplicator = 1.0f;
	[SerializeField]
	private List<GameMaster.EDirection> m_allowedDirections = new List<GameMaster.EDirection>();

	[NonSerialized]
	private float m_elapsedTime = 0.0f;
}