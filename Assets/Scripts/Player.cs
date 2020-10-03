using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	public enum EKeyPressed
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	public void Update()
	{
		if (!m_isPressingKey)
		{
			if (Input.GetKeyDown(KeyCode.UpArrow))
			{
				m_actionManager.ExecuteNext(this, EKeyPressed.UP);
			}
			else if (Input.GetKeyDown(KeyCode.DownArrow))
			{
				m_actionManager.ExecuteNext(this, EKeyPressed.DOWN);
			}
			else if (Input.GetKeyDown(KeyCode.RightArrow))
			{
				m_actionManager.ExecuteNext(this, EKeyPressed.RIGHT);
			}
			else if (Input.GetKeyDown(KeyCode.LeftArrow))
			{
				m_actionManager.ExecuteNext(this, EKeyPressed.LEFT);
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

	[SerializeField]
	private ActionManager m_actionManager = null;

	private bool m_isPressingKey = true;
}