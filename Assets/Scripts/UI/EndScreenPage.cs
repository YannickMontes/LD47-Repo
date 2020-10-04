using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndScreenPage : MonoBehaviour
{
	public void Replay()
	{
		GameMaster.Instance.Replay();
	}

	public void Quit()
	{
		Application.Quit();
	}

	private void OnEnable()
	{
		m_scoreText.text = $"Score:  {ScoreManager.Instance.Score}";
	}

	[SerializeField]
	private TextMeshProUGUI m_scoreText = null;
}