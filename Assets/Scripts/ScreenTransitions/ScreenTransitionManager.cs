using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransitionManager : MonoBehaviour
{
    [SerializeField]
    private ScreenFader screenFader;
    [SerializeField]
    private ScreenWiperAlphaThreshold screenWiper;

    public static ScreenTransitionManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (this != instance)
        {
            Destroy(gameObject);
            return;
        }

        if (transform.parent == null)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public IEnumerator FadeOutCoroutine()
    {
        yield return screenFader.FadeOut();
    }

    public void FadeOut(float fadeTime = 1.0f)
    {
        StartCoroutine(screenFader.FadeOut(fadeTime));
    }

    public void FadeIn(float fadeTime = 1.0f)
    {
        StartCoroutine(screenFader.FadeIn(fadeTime));
    }
}
