using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InGamePage : MonoBehaviour
{
	private void Update()
	{
		m_scoreText.text = $"Score {ScoreManager.Instance.Score}";
	}

	[SerializeField]
	private TextMeshProUGUI m_scoreText = null;
}