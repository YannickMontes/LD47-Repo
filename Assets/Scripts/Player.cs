using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : GameEntity
{
	public ActionManager ActionManager { get { return m_actionManager; } }

	public void Reset()
	{
		m_isPressingKey = false;
		ActionManager.Reset();
	}

	public void InitActions(List<ActionAsset> actionAssets)
	{
		m_actionManager.InitActions(actionAssets);
	}

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
			GameMaster.Instance.OnPlayerHit();
		}
	}

	public void UpdateSprite()
	{
		switch (m_actionManager.m_actions[m_actionManager.m_nextIndex].m_asset.name)
		{
			case "Move":
				m_playerSprite.sprite = m_sprites[0];
				break;

			case "Shield":
				m_playerSprite.sprite = m_sprites[1];
				break;

			case "Swap":
				m_playerSprite.sprite = m_sprites[2];
				break;

			case "Hit":
				m_playerSprite.sprite = m_sprites[3];
				break;
		}
	}

	[SerializeField]
	private ActionManager m_actionManager = null;

	[SerializeField]
	private List<Sprite> m_sprites = new List<Sprite>();

	[SerializeField]
	private SpriteRenderer m_playerSprite = null;

	private bool m_isPressingKey = false;
}