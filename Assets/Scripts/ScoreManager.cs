using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class ScoreManager : Singleton<ScoreManager>
{
	public int Score { get { return m_score; } }

	public void IncreaseScore(int increase)
	{
		m_score += increase;
	}

	private void ResetScore()
	{
		m_score = 0;
	}

	private void Start()
	{
		GameMaster.Instance.GameStateRelay.AddListener(OnGameStateChanged);
	}

	private void OnDestroy()
	{
		GameMaster.Instance?.GameStateRelay.RemoveListener(OnGameStateChanged);
	}

	private void OnGameStateChanged(GameMaster.GameState gameState)
	{
		switch (gameState)
		{
			case GameMaster.GameState.IN_GAME:
				ResetScore();
				StartCoroutine(ScoreOnTime());
				break;

			case GameMaster.GameState.END_SCREEN:
				StopAllCoroutines();
				break;
		}
	}

	private void StartGame()
	{
		StartCoroutine(ScoreOnTime());
	}

	private void StopGame()
	{
		StopAllCoroutines();
	}

	private IEnumerator ScoreOnTime()
	{
		while (true)
		{
			yield return new WaitForSeconds(m_timeUnit);
			m_score += m_scorePerTimeUnit;
		}
	}

	[SerializeField]
	private int m_scorePerTimeUnit = 1;
	[SerializeField]
	private float m_timeUnit = 0.25f;

	[NonSerialized]
	private int m_score = 0;
}