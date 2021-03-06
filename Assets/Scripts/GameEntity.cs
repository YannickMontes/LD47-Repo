﻿using Yube;
using UnityEngine;
using System;

public abstract class GameEntity : MonoBehaviour
{
	public int UpdateEveryTicTime { get { return m_updateEveryTicTime; } }

	private void OnEnable()
	{
		m_elapsedTic = 0;
		UpdateEntityManager.Instance.AddEntity(this);
	}

	protected abstract void Do();

	public void OnUpdateTic()
	{
		m_elapsedTic++;
		if (m_elapsedTic == m_updateEveryTicTime)
		{
			Do();
			m_elapsedTic = 0;
		}
	}

	public virtual void Hit()
	{
		ResourceManager.Instance.ReleaseInstance(this);
	}

	private void OnDisable()
	{
		UpdateEntityManager.Instance.RemoveEntity(this);
		Cell previousCell = GameMaster.Instance.Grid.GetCell(transform.position);
		if (previousCell != null)
		{
			previousCell.RemoveEntity(this);
		}
		m_elapsedTic = 0;
	}

	public void CheckGridState()
	{
		Cell currentCell = GameMaster.Instance.Grid.GetCell(transform.position);
		if (currentCell != null)
		{
			currentCell.AddEntity(this);
		}
	}

	public virtual void Move(Vector2 fromPosition, Vector2 toPosition)
	{
		if (!gameObject.activeSelf)
			return;

		Cell previousCell = GameMaster.Instance.Grid.GetCell(fromPosition);
		if (previousCell != null)
		{
			previousCell.RemoveEntity(this);
		}
		Cell nextCell = GameMaster.Instance.Grid.GetCell(toPosition);
		if (nextCell != null)
		{
			nextCell.AddEntity(this);
		}
		transform.position = toPosition;
	}

	[SerializeField]
	private int m_updateEveryTicTime = 10;

	[NonSerialized]
	private int m_elapsedTic = 0;
}