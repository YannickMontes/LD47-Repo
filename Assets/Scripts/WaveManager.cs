using Yube;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
	public void OnTicUpdate(float ticNumber)
	{
		if (m_canSpawnNextHazard)
		{
			Hazard prefabToSpawn = null;
			m_hazardsToSpawn.Shuffle();
			foreach (Hazard hazardToSpawn in m_hazardsToSpawn)
			{
				if (ticNumber % hazardToSpawn.UpdateEveryTicTime == 0)
				{
					prefabToSpawn = hazardToSpawn;
					break;
				}
			}
			if (prefabToSpawn == null)
			{
				return;
			}
			m_hazardsToSpawn.Remove(prefabToSpawn);
			bool hasEntityOnCell = true;
			GameMaster.EDirection chooseDirection = default(GameMaster.EDirection);
			Vector3 spawnPoint = Vector3.zero;
			int tryLimit = 0;
			while (hasEntityOnCell)
			{
				chooseDirection = prefabToSpawn.AllowedDirections[UnityEngine.Random.Range(0, prefabToSpawn.AllowedDirections.Count)];
				spawnPoint = GameMaster.Instance.GetSpawnPosition(chooseDirection);
				hasEntityOnCell = GameMaster.Instance.Grid.GetCell((int)spawnPoint.x, (int)spawnPoint.y).Entities.Count > 0;
				if (tryLimit > 1000)
				{
					tryLimit = 0;
					return;//Spawn on next frame
				}
				else
				{
					tryLimit++;
				}
			}
			Hazard hazard = ResourceManager.Instance.AcquireInstance(prefabToSpawn, null);
			hazard.transform.position = spawnPoint;
			switch (chooseDirection)
			{
				case GameMaster.EDirection.UP:
					hazard.transform.right = Vector2.down;
					break;

				case GameMaster.EDirection.DOWN:
					hazard.transform.right = Vector2.up;
					break;

				case GameMaster.EDirection.RIGHT:
					hazard.transform.right = Vector2.left;
					break;

				case GameMaster.EDirection.LEFT:
					hazard.transform.right = Vector2.right;
					break;
			}
			hazard.Init(chooseDirection);
			m_canSpawnNextHazard = false;
		}
	}

	public void Reset()
	{
		StopAllCoroutines();
		m_hazardsToSpawn.Clear();
		m_waveNumber = 0;
	}

	public void SpawnWave()
	{
		Debug.Log("New wave " + m_waveNumber);
		m_waveNumber++;
		FillHazardsToSpawn();
		StartCoroutine(WaitTimeForNextSpawn());
	}

	private void FillHazardsToSpawn()
	{
		int minNbHazards = m_minHazards + ((m_waveNumber - 1) * m_intervalIncrease);
		int maxNbHazards = m_minHazards + (m_waveNumber * m_intervalIncrease);
		/** **BASIC RANDOM **/
		m_hazardsToSpawn.Clear();
		int nbHazard = UnityEngine.Random.Range(minNbHazards, maxNbHazards + 1);
		while (m_hazardsToSpawn.Count < nbHazard)
		{
			int prefabIndex = UnityEngine.Random.Range(0, m_hazardsPrefabs.Count);
			m_hazardsToSpawn.Add(m_hazardsPrefabs[prefabIndex]);
		}
	}

	private IEnumerator WaitTimeForNextSpawn()
	{
		while (m_hazardsToSpawn.Count > 0)
		{
			float timeToWait = UnityEngine.Random.Range(m_minTimeBetweenSpawn, m_maxTimeBetweenSpawn);
			yield return new WaitForSeconds(timeToWait);
			m_canSpawnNextHazard = true;
		}
		m_canSpawnNextHazard = false;
	}

	[SerializeField]
	public float m_delayBetweenWaves = 30;

	[SerializeField]
	private int m_minHazards = 5;

	[SerializeField]
	private int m_intervalIncrease = 3;

	[SerializeField]
	private float m_minTimeBetweenSpawn = 0.2f;

	[SerializeField]
	private float m_maxTimeBetweenSpawn = 1.0f;

	[SerializeField]
	private List<Hazard> m_hazardsPrefabs = new List<Hazard>();

	[NonSerialized]
	private bool m_canSpawnNextHazard = false;

	[NonSerialized]
	private int m_waveNumber = 0;

	[NonSerialized]
	private List<Hazard> m_hazardsToSpawn = new List<Hazard>();
}