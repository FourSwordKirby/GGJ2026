using UnityEngine;
using System;
using System.Collections.Generic;

public class EndingUI : MonoBehaviour
{
	public static event Action OnComplete;

	void Start()
	{
		OnComplete?.Invoke();
	}
}