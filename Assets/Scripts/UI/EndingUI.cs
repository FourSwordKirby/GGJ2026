using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EndingUI : MonoBehaviour
{
	public static event Action OnComplete;
	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void OnEnable()
	{
		GameManager.OnGameComplete += Kickoff;
	}

	void OnDisable()
	{
		GameManager.OnGameComplete -= Kickoff;
	}

	void Kickoff()
	{
		StartCoroutine(EndingSequence());
	}

	IEnumerator EndingSequence()
	{
		// Slide one

		gameObject.SetActive(true);
		animator.SetTrigger("seq1");
		yield return null;

		while(!InputHelper.wasPressed("Toggle"))
			yield return null;

		// Slide two

		animator.SetTrigger("seq2");
		yield return null;

		while(!InputHelper.wasPressed("Toggle"))
			yield return null;


		// Final slide

		animator.SetTrigger("seq3");
		yield return null;

		while(!InputHelper.wasPressed("Toggle"))
			yield return null;

		// Exit

		animator.SetTrigger("seq4");
		yield return null;

		while(!InputHelper.wasPressed("Toggle"))
			yield return null;

		yield return new WaitForSeconds(0.5f);

		OnComplete?.Invoke();

		// yield return new WaitForSeconds(1.0f);
		gameObject.SetActive(false);
	}
}