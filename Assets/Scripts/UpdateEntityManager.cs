using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class UpdateEntityManager : Singleton<UpdateEntityManager>
{
	public void AddEntity(GameEntity entity)
	{
		m_entities.Add(entity);
	}

	public void RemoveEntity(GameEntity entity)
	{
		m_entities.Remove(entity);
	}

	public void StartUpdate()
	{
		StartCoroutine(TicTime());
	}

	public void StopUpdate()
	{
		StopAllCoroutines();
	}

	private IEnumerator TicTime()
	{
		while (true)
		{
			yield return new WaitForEndOfFrame();
			m_nbTics++;
			for (int i = m_entities.Count - 1; i >= 0; i--)
			{
				try
				{
					if (i >= m_entities.Count)
					{
						continue;
					}
					if (m_entities[i] == null)
					{
						Debug.LogError("Null entity on update manager... Skiping");
						continue;
					}
				}
				catch (System.ArgumentOutOfRangeException ex)
				{
					Debug.Log("lul");
				}
				m_entities[i].OnUpdateTic();
			}
			WaveManager.Instance.OnTicUpdate(m_nbTics);
		}
	}

	[SerializeField]
	private float m_ticTime = 0.1f;

	[NonSerialized]
	private List<GameEntity> m_entities = new List<GameEntity>();
	[NonSerialized]
	private int m_nbTics = 0;
}