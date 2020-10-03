using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yube;

public class GameMaster : Singleton<GameMaster>
{
	public enum EDirection
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	public Transform GetSpawnPosition(EDirection direction)
	{
		Transform spawnPoint = null;
		switch (direction)
		{
			case EDirection.UP:
				spawnPoint = m_upSpawns[UnityEngine.Random.Range(0, m_upSpawns.Count)];
				break;

			case EDirection.DOWN:
				spawnPoint = m_bottomSpawns[UnityEngine.Random.Range(0, m_bottomSpawns.Count)];
				break;

			case EDirection.RIGHT:
				spawnPoint = m_rightSpawns[UnityEngine.Random.Range(0, m_rightSpawns.Count)];
				break;

			case EDirection.LEFT:
				spawnPoint = m_leftSpawns[UnityEngine.Random.Range(0, m_leftSpawns.Count)];
				break;
		}
		return spawnPoint;
	}

	public void Reload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		CreateWaveManager();
	}

	public void Start()
	{
		CreateWaveManager();
	}

	private void CreateWaveManager()
	{
		if (m_waveManager == null)
		{
			m_waveManager = ResourceManager.Instance.AcquireInstance(m_waveManagerPrefab, null);
		}
		CreateWave();
	}

	private void CreateWave()
	{
		m_waveManager.SpawnWave();
		m_canLaunchNextWave = false;
		StartCoroutine(WaitForNextWave());
	}

	private void Update()
	{
		CheckReload();

		if (m_canLaunchNextWave == true)
			CreateWave();
	}

	private void CheckReload()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			Reload();
		}
	}

	private IEnumerator WaitForNextWave()
	{
		yield return new WaitForSeconds(m_waveManager.m_delayBetweenWaves);

		m_canLaunchNextWave = true;
	}

	[NonSerialized]
	private bool m_canLaunchNextWave = true;

	[SerializeField]
	private WaveManager m_waveManagerPrefab = null;

	[SerializeField]
	private List<Transform> m_upSpawns = new List<Transform>();

	[SerializeField]
	private List<Transform> m_rightSpawns = new List<Transform>();

	[SerializeField]
	private List<Transform> m_bottomSpawns = new List<Transform>();

	[SerializeField]
	private List<Transform> m_leftSpawns = new List<Transform>();

	[NonSerialized]
	private WaveManager m_waveManager = null;
}