using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
	public void Awake()
	{
		m_actions = new List<ActionInstance>(m_actionsAssets.Count);
		foreach (ActionAsset asset in m_actionsAssets)
		{
			m_actions.Add(asset.CreateInstance());
		}
	}

	public void ExecuteNext(Player player, GameMaster.EDirection keyPressed)
	{
		m_currentAction = m_actions[m_nextIndex];
		m_currentAction.OnEndRelay.AddListener(OnEndAction);
		m_currentAction.Execute(player, keyPressed);
	}

	public void Update()
	{
		m_currentAction?.Update();
	}

	private void OnEndAction(bool success)
	{
		m_currentAction.OnEndRelay.RemoveListener(OnEndAction);
		m_currentAction = null;
		if (success)
		{
			m_nextIndex++;
			if (m_nextIndex >= m_actions.Count)
			{
				m_nextIndex = 0;
			}
		}
	}

	[SerializeField]
	private List<ActionAsset> m_actionsAssets = new List<ActionAsset>();

	[NonSerialized]
	public List<ActionInstance> m_actions = null;

	[NonSerialized]
	public ActionInstance m_currentAction = null;

	[NonSerialized]
	public int m_nextIndex = 0;
}