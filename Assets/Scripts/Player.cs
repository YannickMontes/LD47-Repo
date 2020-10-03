using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : GameEntity
{
	public void Update()
	{
		if (!m_isPressingKey)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				m_actionManager.ExecuteNext(this, GameMaster.EDirection.UP);
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				m_actionManager.ExecuteNext(this, GameMaster.EDirection.DOWN);
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				m_actionManager.ExecuteNext(this, GameMaster.EDirection.RIGHT);
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				m_actionManager.ExecuteNext(this, GameMaster.EDirection.LEFT);
			}
		}
		else
		{
			CheckKeyReleased();
		}
	}

	private void CheckKeyReleased()
	{
		if (Input.GetKeyUp(KeyCode.UpArrow)
			|| Input.GetKeyUp(KeyCode.DownArrow)
			|| Input.GetKeyUp(KeyCode.RightArrow)
			|| Input.GetKeyUp(KeyCode.LeftArrow))
		{
			m_isPressingKey = false;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Hazard")
		{
			Debug.Log("Coll with enemy ! Reload");
			GameMaster.Instance.Reload();
		}
	}

	[SerializeField]
	private ActionManager m_actionManager = null;

	private bool m_isPressingKey = false;
}