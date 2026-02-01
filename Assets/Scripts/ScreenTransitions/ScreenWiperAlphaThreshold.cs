using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenWiperAlphaThreshold : MonoBehaviour
{
    public Image screen;

    public List<Sprite> screenWipeTypes;

    private float wipeProgress; 
    public bool fading;

    public IEnumerator FadeOut(float fadeTime = 1.0f)
    {
        // Used to ensure we don't fade out while fading in and vice versa
        while (fading)
        {
            yield return null;
        }

        fading = true;
        float timer = 0.0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            if (timer < fadeTime)
            {
                wipeProgress = Mathf.Lerp(0, 1, timer / fadeTime);
                screen.materialForRendering.SetFloat("_AlphaThreshold", wipeProgress);
                yield return new WaitForEndOfFrame();
            }
        }
        wipeProgress = 1;
        screen.materialForRendering.SetFloat("_AlphaThreshold", wipeProgress);
        fading = false;

        yield return null;
    }

    public IEnumerator FadeIn(float fadeTime = 1.0f)
    {
        // Used to ensure we don't fade out while fading in and vice versa
        while (fading)
        {
            yield return null;
        }

        fading = true;
        float timer = 0.0f;

        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            if (timer < fadeTime)
            {
                wipeProgress = Mathf.Lerp(1, 0, timer / fadeTime);
                screen.materialForRendering.SetFloat("_AlphaThreshold", wipeProgress);
                yield return new WaitForEndOfFrame();
            }
        }
        wipeProgress = 0;
        screen.materialForRendering.SetFloat("_AlphaThreshold", wipeProgress);
        fading = false;

        yield return null;
    }
}
