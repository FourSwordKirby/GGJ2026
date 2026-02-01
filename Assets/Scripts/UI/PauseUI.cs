using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public static class InputHelper
{
	public static bool wasPressed(string actionName)
	{
		// Needed as IsPressed is true beyond the first frame

		InputAction action = InputSystem.actions.FindAction(actionName);
		return action.triggered && action.ReadValue<float>() > 0;
	}
}

public class PauseUI : MonoBehaviour
{
	Animator animator;

	bool paused = false;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void Update()
	{
		if (paused)
		{
			if (InputHelper.wasPressed("Pause") ||
				InputHelper.wasPressed("Toggle") ||
				InputHelper.wasPressed("Cancel"))
			{
				SetPause(false);
			}
		}
		else
		{
			if (InputHelper.wasPressed("Pause"))
			{
				// Only allow during gameplay

				GameManager.GamePhase phase = GameManager.instance.currentPhase;

				if (phase != GameManager.GamePhase.PeriodInProgress)
					return;

				SetPause(true);
			}
		}
	}

	void SetPause(bool pausedNext)
	{
		if (paused == pausedNext)
			return;

		paused = pausedNext;
		animator.SetBool("paused", paused);

		if (paused)
		{
			Time.timeScale = 0;
		}
		else
		{
			Time.timeScale = 1;
		}
	}
}