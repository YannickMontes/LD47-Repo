using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yube.Relays;

public abstract class ActionInstance
{
	public IRelayLink OnEndRelay { get { return m_endRelay ?? (m_endRelay = new Relay()); } }

	public abstract void Execute(Player player, Player.EKeyPressed keyPressed);

	public virtual void Update()
	{
	}

	protected ActionInstance(ActionAsset asset)
	{
		m_asset = asset;
	}

	protected void OnFinishAction()
	{
		m_endRelay?.Dispatch();
	}

	private ActionAsset m_asset = null;
	private Relay m_endRelay = null;
}