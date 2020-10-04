using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionAsset : ScriptableObject
{
	public Sprite Sprite { get { return m_sprite; } }

	public abstract ActionInstance CreateInstance();

	[SerializeField]
	private string m_name;
	[SerializeField]
	private Sprite m_sprite;
	[SerializeField]
	public AudioClip m_clip;
}