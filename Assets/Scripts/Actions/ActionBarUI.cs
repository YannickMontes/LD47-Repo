using Yube;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarUI : Singleton<ActionBarUI>
{
	private void OnEnable()
	{
		InitUI();
		UpdateUI();
	}

	private void Update()
	{
		UpdateUI();
	}

	private void InitUI()
	{
		for (int ui = 0; ui < m_slotsList.Count; ui++)
		{
			m_slotsList[ui].sprite = GameMaster.Instance.Player.ActionManager.m_actions[ui].Asset.Sprite;
		}
	}

	private void UpdateUI()
	{
		if (m_currentIndex != GameMaster.Instance.Player.ActionManager.m_nextIndex)
		{
			GameObject go = m_slotsList[GameMaster.Instance.Player.ActionManager.m_nextIndex].gameObject;
			Transform bgTransform = m_spriteBackground.transform;
			bgTransform.position = go.transform.position;
			m_currentIndex = GameMaster.Instance.Player.ActionManager.m_nextIndex;
		}
	}

	[SerializeField]
	private List<Image> m_slotsList = new List<Image>();
	[SerializeField]
	private GameObject m_spriteBackground = null;

	[NonSerialized]
	public int m_currentIndex = -1;
}