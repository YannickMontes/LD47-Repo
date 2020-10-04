using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yube;
using Yube.Relays;

public class GameMaster : Singleton<GameMaster>
{
	public enum EDirection
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	public enum GameState
	{
		CHOOSE_SEQUENCE,
		IN_GAME,
		END_SCREEN
	}

	public Grid Grid { get { return m_grid; } }
	public IRelayLink<GameState> GameStateRelay { get { return m_gameStateRelay ?? (m_gameStateRelay = new Relay<GameState>()); } }

	public bool CanPlayerMove(int x, int y)
	{
		Cell cell = m_grid.GetCell(x, y);
		return cell != null && !cell.Contains<Shield>();
	}

	public Vector3 GetSpawnPosition(EDirection direction)
	{
		Vector3 spawnPoint = Vector3.zero;
		switch (direction)
		{
			case EDirection.UP:
				spawnPoint = new Vector3(UnityEngine.Random.Range(0, Grid.X), Grid.Y - 1, 0.0f);
				break;

			case EDirection.DOWN:
				spawnPoint = new Vector3(UnityEngine.Random.Range(0, Grid.X), 0.0f, 0.0f);
				break;

			case EDirection.RIGHT:
				spawnPoint = new Vector3(Grid.X - 1, UnityEngine.Random.Range(0, Grid.Y), 0.0f);
				break;

			case EDirection.LEFT:
				spawnPoint = new Vector3(0.0f, UnityEngine.Random.Range(0, Grid.Y), 0.0f);
				break;
		}
		return spawnPoint;
	}

	public void Reload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		CreateWaveManager();
	}

	private void Start()
	{
		m_gameState = GameState.CHOOSE_SEQUENCE;
		m_grid = new Grid(m_xSize, m_ySize, m_pairBox, m_oddBox);
		CreateWaveManager();
	}

	protected void ChangeState(GameState newState)
	{
		m_gameState = newState;
		m_gameStateRelay?.Dispatch(newState);
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
	[SerializeField]
	public GameObject m_pairBox;
	[SerializeField]
	public GameObject m_oddBox;

	[Header("Wave")]
	[SerializeField]
	private WaveManager m_waveManagerPrefab = null;

	[NonSerialized]
	private GameState m_gameState = default(GameState);
	[NonSerialized]
	private WaveManager m_waveManager = null;
	[NonSerialized]
	private Grid m_grid = null;
	[NonSerialized]
	private bool m_canLaunchNextWave = true;
	[NonSerialized]
	private Relay<GameState> m_gameStateRelay = null;
}