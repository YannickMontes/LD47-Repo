﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChooseSequencePage : MonoBehaviour
{
	public void Play()
	{
		if (GetFirstButtonNotFilled() == null)
		{
			List<ActionAsset> sequenceActions = new List<ActionAsset>();
			foreach (ActionButtonUI actionButtonUI in m_finalSequence)
			{
				sequenceActions.Add(actionButtonUI.ActionAsset);
			}
			GameMaster.Instance.LaunchGame(sequenceActions);
		}
	}

	private void OnEnable()
	{
		GenerateRandomActions();
		m_selectedButtonsDict.Clear();
		foreach (ActionButtonUI actionButtonUI in m_finalSequence)
		{
			actionButtonUI.SetAction(null);
			actionButtonUI.OnClickRelay.AddListener(OnSequenceButtonClick);
		}
		for (int i = 0; i < m_generatedSequence.Count; i++)
		{
			m_generatedSequence[i].OnClickRelay.AddListener(OnGeneratedButtonClick);
			m_generatedSequence[i].SetAction(m_generatedActions[i]);
			m_generatedSequence[i].EnableInteraction(true);
		}
		foreach (ActionButtonUI actionButtonUI in m_generatedSequence)
		{
			actionButtonUI.OnClickRelay.AddListener(OnGeneratedButtonClick);
		}
		CheckPlayButtonInteractability();
	}

	private void OnDisable()
	{
		foreach (ActionButtonUI actionButtonUI in m_finalSequence)
		{
			actionButtonUI.OnClickRelay.RemoveListener(OnSequenceButtonClick);
		}
		foreach (ActionButtonUI actionButtonUI in m_generatedSequence)
		{
			actionButtonUI.OnClickRelay.RemoveListener(OnGeneratedButtonClick);
		}
	}

	private void OnGeneratedButtonClick(ActionButtonUI clickedButton)
	{
		ActionButtonUI notFilledButton = GetFirstButtonNotFilled();
		if (notFilledButton != null)
		{
			notFilledButton.SetAction(clickedButton.ActionAsset);
			m_selectedButtonsDict.Add(clickedButton, notFilledButton);
			clickedButton.EnableInteraction(false);
		}
		CheckPlayButtonInteractability();
	}

	private void OnSequenceButtonClick(ActionButtonUI clickedButton)
	{
		ActionButtonUI keyToRemove = null;
		foreach (KeyValuePair<ActionButtonUI, ActionButtonUI> pair in m_selectedButtonsDict)
		{
			if (pair.Value == clickedButton)
			{
				keyToRemove = pair.Key;
				break;
			}
		}
		if (keyToRemove != null)
		{
			keyToRemove.EnableInteraction(true);
			m_selectedButtonsDict[keyToRemove].SetAction(null);
			m_selectedButtonsDict.Remove(keyToRemove);
		}
		CheckPlayButtonInteractability();
	}

	private ActionButtonUI GetFirstButtonNotFilled()
	{
		for (int i = 0; i < m_finalSequence.Count; i++)
		{
			if (!m_selectedButtonsDict.ContainsValue(m_finalSequence[i]))
			{
				return m_finalSequence[i];
			}
		}
		return null;
	}

	private void GenerateRandomActions()
	{
		m_generatedActions.Clear();
		foreach (ActionAsset actionAsset in m_mandatoryActions)
		{
			m_generatedActions.Add(actionAsset);
		}
		while (m_generatedActions.Count < m_generatedSequence.Count)
		{
			ActionAsset choosedAction = m_actions[UnityEngine.Random.Range(0, m_actions.Count)];
			while (choosedAction.name == "Swap")
			{
				int cpt = 0;
				foreach (ActionAsset asset in m_generatedActions)
				{
					if (asset.name == "Swap")
						cpt++;
				}
				if (cpt < 2)
				{
					break;
				}
				else
				{
					choosedAction = m_actions[UnityEngine.Random.Range(0, m_actions.Count)];
				}
			}
			m_generatedActions.Add(choosedAction);
		}
		m_generatedActions.Shuffle();
	}

	private void CheckPlayButtonInteractability()
	{
		m_playButton.interactable = m_finalSequence.Count == m_selectedButtonsDict.Count;
	}

	[Header("Random generation")]
	[SerializeField]
	private List<ActionAsset> m_actions = new List<ActionAsset>();
	[SerializeField]
	private List<ActionAsset> m_mandatoryActions = new List<ActionAsset>();

	[Header("Buttons")]
	[SerializeField]
	private List<ActionButtonUI> m_generatedSequence = new List<ActionButtonUI>();
	[SerializeField]
	private List<ActionButtonUI> m_finalSequence = new List<ActionButtonUI>();
	[SerializeField]
	private Button m_playButton = null;

	[NonSerialized]
	private Dictionary<ActionButtonUI, ActionButtonUI> m_selectedButtonsDict = new Dictionary<ActionButtonUI, ActionButtonUI>(); //Clicked => final
	[NonSerialized]
	private List<ActionAsset> m_generatedActions = new List<ActionAsset>();
}