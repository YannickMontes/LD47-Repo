using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActionAsset : ScriptableObject
{
	public abstract ActionInstance CreateInstance();

	[SerializeField]
	private string m_name;

	[SerializeField]
	public AudioClip m_clip;
}