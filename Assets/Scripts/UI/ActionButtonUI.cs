using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yube.Relays;

public class ActionButtonUI : MonoBehaviour
{
	public IRelayLink<ActionButtonUI> OnClickRelay { get { return m_onClickRelay ?? (m_onClickRelay = new Relay<ActionButtonUI>()); } }
	public ActionAsset ActionAsset { get { return m_actionAsset; } }

	public void OnClick()
	{
		m_onClickRelay?.Dispatch(this);
	}

	public void EnableInteraction(bool enable)
	{
		m_button.interactable = enable;
	}

	public void SetAction(ActionAsset actionAsset)
	{
		m_actionAsset = actionAsset;
		m_image.sprite = actionAsset != null ? actionAsset.Sprite : m_defaultSprite;
	}

	private void OnEnable()
	{
		m_image.sprite = m_actionAsset != null ? m_actionAsset.Sprite : m_defaultSprite;
	}

	[SerializeField]
	private Image m_image = null;
	[SerializeField]
	private Button m_button = null;
	[SerializeField]
	private Sprite m_defaultSprite = null;

	[NonSerialized]
	private ActionAsset m_actionAsset = null;
	[NonSerialized]
	private Relay<ActionButtonUI> m_onClickRelay = null;
}