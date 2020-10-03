using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
	public void Update()
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

	[NonSerialized]
	private float m_elapsedTime = 0.0f;
}