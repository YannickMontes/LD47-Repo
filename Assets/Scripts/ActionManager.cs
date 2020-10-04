using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

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

	public void PlayActionSound(AudioClip clip)
	{
		if (clip == null || m_audioSound == null)
			return;
		m_audioSound.PlayClip(clip);
		return;
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
		if (success)
		{
			PlayActionSound(m_currentAction.Asset.m_clip);
			m_nextIndex++;
			if (m_nextIndex >= m_actions.Count)
			{
				m_nextIndex = 0;
			}
		}
		m_currentAction = null;
	}

	[SerializeField]
	private List<ActionAsset> m_actionsAssets = new List<ActionAsset>();

	[SerializeField]
	private AudioSound m_audioSound = null;

	[NonSerialized]
	public List<ActionInstance> m_actions = null;

	[NonSerialized]
	public ActionInstance m_currentAction = null;

	[NonSerialized]
	public int m_nextIndex = 0;
}