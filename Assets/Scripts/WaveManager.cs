using Yube;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : Singleton<WaveManager>
{
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
		StartCoroutine(SpawnHazards());
	}

	private void FillHazardsToSpawn()
	{
		m_hazardsToSpawn.Clear();
		int minNbHazards = m_minHazards + ((m_waveNumber - 1) * m_intervalIncrease);
		int maxNbHazards = m_minHazards + (m_waveNumber * m_intervalIncrease);
		int nbHazard = UnityEngine.Random.Range(minNbHazards, maxNbHazards + 1);
		while (m_hazardsToSpawn.Count < nbHazard)
		{
			int prefabIndex = UnityEngine.Random.Range(0, m_hazardsPrefabs.Count);
			m_hazardsToSpawn.Enqueue(m_hazardsPrefabs[prefabIndex]);
		}
	}

	private IEnumerator SpawnHazards()
	{
		while (m_hazardsToSpawn.Count > 0)
		{
			Hazard prefab = m_hazardsToSpawn.Dequeue();
			GameMaster.EDirection chooseDirection = prefab.AllowedDirections[UnityEngine.Random.Range(0, prefab.AllowedDirections.Count)];
			Transform spawnPoint = GameMaster.Instance.GetSpawnPosition(chooseDirection);
			Hazard hazard = ResourceManager.Instance.AcquireInstance(prefab, spawnPoint);
			hazard.transform.position = spawnPoint.position;
			float timeToWait = UnityEngine.Random.Range(m_minTimeBetweenSpawn, m_maxTimeBetweenSpawn);
			yield return new WaitForSeconds(timeToWait);
		}
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
	private int m_waveNumber = 0;

	[NonSerialized]
	private Queue<Hazard> m_hazardsToSpawn = new Queue<Hazard>();
}