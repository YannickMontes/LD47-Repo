using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class ScoreManager : Singleton<ScoreManager>
{
	public int Score { get { return m_score; } }

	public void Reset()
	{
		m_score = 0;
	}

	[NonSerialized]
	private int m_score = 0;
}