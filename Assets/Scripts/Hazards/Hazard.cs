using Yube;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Hazard : GameEntity
{
	public IReadOnlyList<GameMaster.EDirection> AllowedDirections { get { return m_allowedDirections; } }

	public virtual void Init(GameMaster.EDirection direction)
	{
		m_elapsedTime = 0.0f;
		m_spawnedDirection = direction;
		CheckGridState();
	}

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

	protected void OnTriggerEnter2D(Collider2D collision)
	{
		switch (collision.tag)
		{
			case "Player":
				OnPlayerCollide();
				break;

			case "Hazard":
				OnHazardCollide();
				break;

			case "Shield":
				OnShieldCollide();
				break;
		}
	}

	protected virtual void OnHazardCollide()
	{
		ResourceManager.Instance.ReleaseInstance(this);
	}

	protected virtual void OnPlayerCollide()
	{
		ResourceManager.Instance.ReleaseInstance(this);
	}

	protected virtual void OnShieldCollide()
	{
	}

	protected void InvertDirection()
	{
		transform.right = transform.right * -1;
	}

	[SerializeField]
	private float m_ticMultiplicator = 1.0f;
	[SerializeField]
	private List<GameMaster.EDirection> m_allowedDirections = new List<GameMaster.EDirection>();

	[NonSerialized]
	private float m_elapsedTime = 0.0f;
	[NonSerialized]
	protected GameMaster.EDirection m_spawnedDirection;
}