using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube;

public class AudioSound : MonoBehaviour
{
	public void PlayClip(AudioClip clip)
	{
		m_audioSource.clip = clip;
		m_audioSource.Play();
	}

	#region private

	private void Awake()
	{
		m_audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
	}

	private AudioSource m_audioSource = null;

	#endregion private
}