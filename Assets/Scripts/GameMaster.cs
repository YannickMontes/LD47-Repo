using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yube;

public class GameMaster : Singleton<GameMaster>
{
	public void Reload()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void Update()
	{
		CheckReload();
	}

	private void CheckReload()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			Reload();
		}
	}
}