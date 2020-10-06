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
	public Player Player { get { return m_player; } }

	public bool CanPlayerMove(Vector2 coord)
	{
		Cell cell = m_grid.GetCell(coord);
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

	public void LaunchGame(List<ActionAsset> playerActions)
	{
		m_grid = new Grid(m_xSize, m_ySize, m_backgroundBox, m_pairBox, m_oddBox);
		CreateWaveManager();
		m_canLaunchNextWave = true;
		m_player = ResourceManager.Instance.AcquireInstance(m_playerPrefab, null);
		m_player.transform.position = new Vector2((int)(Grid.X / 2.0f), (int)(Grid.Y / 2.0f));
		m_player.InitActions(playerActions);
		UpdateEntityManager.Instance.StartUpdate();
		m_player.ActionManager.UpdatePlayerSprite(m_player);
		ChangeState(GameState.IN_GAME);
	}

	public void Replay()
	{
		ChangeState(GameState.CHOOSE_SEQUENCE);
	}

	public void OnPlayerHit()
	{
		m_waveManager.Reset();
		ResourceManager.Instance.ReleaseInstance(m_waveManager);
		m_waveManager = null;
		m_player.Reset();
		ResourceManager.Instance.ReleaseInstance(m_player);
		m_player = null;
		m_grid.Destroy();
		m_grid = null;
		UpdateEntityManager.Instance.StopUpdate();
		ChangeState(GameState.END_SCREEN);
	}

	private void Start()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = 30;
		ChangeState(GameState.CHOOSE_SEQUENCE);
	}

	protected void ChangeState(GameState newState)
	{
		m_gameState = newState;
		m_gameStateRelay?.Dispatch(newState);
		if (newState == GameState.IN_GAME)
		{
			m_gameMusicSource.Play();
		}
		else
		{
			m_gameMusicSource.Stop();
		}
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

		if (m_gameState == GameState.IN_GAME)
		{
			if (m_canLaunchNextWave == true)
				CreateWave();
		}
	}

	private void CheckReload()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			OnPlayerHit();
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

	[SerializeField]
	public GameObject m_backgroundBox;

	[Header("Wave")]
	[SerializeField]
	private WaveManager m_waveManagerPrefab = null;

	[Header("Prefabs")]
	[SerializeField]
	private Player m_playerPrefab = null;

	[Header("Audio")]
	[SerializeField]
	private AudioSource m_gameMusicSource = null;

	[NonSerialized]
	private Player m_player = null;

	[NonSerialized]
	private GameState m_gameState = default(GameState);

	[NonSerialized]
	private WaveManager m_waveManager = null;

	[NonSerialized]
	private Grid m_grid = null;

	[NonSerialized]
	private bool m_canLaunchNextWave = false;

	[NonSerialized]
	private Relay<GameState> m_gameStateRelay = null;
}