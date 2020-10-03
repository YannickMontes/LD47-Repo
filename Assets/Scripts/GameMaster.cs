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

	public Grid Grid { get { return m_grid; } }

	public bool CanPlayerMove(int x, int y)
	{
		Cell cell = m_grid.GetCell(x, y);
		return cell != null && !cell.Contains<Shield>();
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

	protected override void Awake()
	{
		base.Awake();
		m_grid = new Grid(m_xSize, m_ySize);
	}

	private void Start()
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

	[Header("Grid")]
	[SerializeField]
	private int m_xSize = 10;
	[SerializeField]
	private int m_ySize = 10;

	[Header("Wave")]
	[SerializeField]
	private WaveManager m_waveManagerPrefab = null;

	[Header("Spawn Points")]
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
	[NonSerialized]
	private Grid m_grid = null;
	[NonSerialized]
	private bool m_canLaunchNextWave = true;
}