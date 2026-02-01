using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class TitleUI : MonoBehaviour
{
	public static event Action OnComplete;

	Animator animator;

	void Awake()
	{
		animator = GetComponent<Animator>();
	}

	void Reset()
	{
		StartCoroutine(TitleSequence());
	}

	void OnEnable()
	{
		Reset();
	}

	IEnumerator TitleSequence()
	{
		gameObject.SetActive(true);
		animator.SetTrigger("intro");

		while(!InputSystem.actions.FindAction("Toggle").IsPressed())
			yield return null;

		animator.SetTrigger("outro");

		yield return new WaitForSeconds(0.5f);

		OnComplete?.Invoke();

		yield return new WaitForSeconds(1.0f);
		gameObject.SetActive(false);
	}
}