using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class UIManager : Singleton<UIManager>
{
	[Serializable]
	public class GameStatePage
	{
		public GameObject Page = null;
		public GameMaster.GameState GameState = default(GameMaster.GameState);
	}

	protected override void Awake()
	{
		base.Awake();
		GameMaster.Instance.GameStateRelay.AddListener(OnGameStateChanged);
		foreach (GameStatePage page in m_uiPages)
		{
			page.Page.SetActive(false);
		}
	}

	private void OnDestroy()
	{
		GameMaster.Instance?.GameStateRelay.RemoveListener(OnGameStateChanged);
	}

	private void OnGameStateChanged(GameMaster.GameState newState)
	{
		foreach (GameStatePage page in m_uiPages)
		{
			page.Page.SetActive(page.GameState == newState);
		}
	}

	[SerializeField]
	private List<GameStatePage> m_uiPages = new List<GameStatePage>();
}