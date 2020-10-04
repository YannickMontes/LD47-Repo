using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class ActionManager : MonoBehaviour
{
	public void InitActions(List<ActionAsset> actionsAssets)
	{
		m_actions = new List<ActionInstance>(actionsAssets.Count);
		foreach (ActionAsset actionAsset in actionsAssets)
		{
			m_actions.Add(actionAsset.CreateInstance());
		}
	}

	public void PlayActionSound(AudioClip clip)
	{
		if (clip == null || m_audioSound == null)
			return;
		m_audioSound.PlayClip(clip);
		return;
	}

	public void UpdatePlayerSprite(Player player)
	{
		player.UpdateSprite();
	}

	public void ExecuteNext(Player player, GameMaster.EDirection keyPressed)
	{
		m_currentAction = m_actions[m_nextIndex];
		m_currentAction.OnEndRelay.AddListener(OnEndAction);
		m_currentAction.Execute(player, keyPressed);
		UpdatePlayerSprite(player);
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
	private AudioSound m_audioSound = null;

	[NonSerialized]
	public List<ActionInstance> m_actions = null;

	[NonSerialized]
	public ActionInstance m_currentAction = null;

	[NonSerialized]
	public int m_nextIndex = 0;
}