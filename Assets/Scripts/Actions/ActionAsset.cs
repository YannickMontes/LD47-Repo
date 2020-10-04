using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionAsset : ScriptableObject
{
	public Sprite Sprite { get { return m_sprite; } }
	public int ScorePoint { get { return m_scorePoint; } }

	public abstract ActionInstance CreateInstance();

	[SerializeField]
	private int m_scorePoint = 1;
	[SerializeField]
	private Sprite m_sprite;
	[SerializeField]
	public AudioClip m_clip;
}