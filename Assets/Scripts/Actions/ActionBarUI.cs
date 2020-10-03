using Yube;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionBarUI : Singleton<ActionBarUI>
{
	// Start is called before the first frame update

	private void Start()
	{
		InitUI();
		UpdateUI();
	}

	// Update is called once per frame
	private void Update()
	{
		UpdateUI();
	}

	private void InitUI()
	{
		for (int ui = 0; ui < m_slotsList.Count; ui++)
		{
			GameObject go = m_slotsList[ui];
			SpriteRenderer spriteRendeder = go.GetComponent<SpriteRenderer>();
			spriteRendeder.sprite = GetActionSprite(m_actionManager.m_actions[ui]);
		}
	}

	private void UpdateUI()
	{
		if (m_currentIndex != m_actionManager.m_nextIndex)
		{
			GameObject go = m_slotsList[m_actionManager.m_nextIndex];
			Transform bgTransform = m_spriteBackground.transform;
			bgTransform.position = go.transform.position;
			m_currentIndex = m_actionManager.m_nextIndex;
		}
	}

	private Sprite GetActionSprite(ActionInstance actionInstance)
	{
		switch (actionInstance.Asset.name)
		{
			case "Move":
				return m_spriteList[0];

			case "Shield":
				return m_spriteList[1];

			case "Swap":
				return m_spriteList[2];
		}

		return m_spriteList[0];
	}

	[SerializeField]
	private ActionManager m_actionManager = null;

	[SerializeField]
	private List<GameObject> m_slotsList = new List<GameObject>();

	[SerializeField]
	private List<Sprite> m_spriteList = new List<Sprite>();

	[SerializeField]
	private GameObject m_spriteBackground;

	[NonSerialized]
	public int m_currentIndex = -1;
}