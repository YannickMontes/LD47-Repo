using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube.Relays;

public abstract class ActionInstance
{
	public ActionAsset Asset { get { return m_asset as ActionAsset; } }

	public IRelayLink<bool, bool> OnEndRelay { get { return m_endRelay ?? (m_endRelay = new Relay<bool, bool>()); } }

	public abstract void Execute(Player player, GameMaster.EDirection keyPressed);

	public virtual void Update()
	{
	}

	public virtual void Reset()
	{
	}

	protected ActionInstance(ActionAsset asset)
	{
		m_asset = asset;
	}

	protected void OnFinishAction(bool success, bool addScore = true)
	{
		m_endRelay?.Dispatch(success, addScore);
	}

	public ActionAsset m_asset = null;
	private Relay<bool, bool> m_endRelay = null;
}