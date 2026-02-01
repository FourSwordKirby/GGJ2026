using MaskGame.Character;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PromptUI : MonoBehaviour
{
    public GameObject indicator;

    public static PromptUI instance;

    public void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public static void ShowMaskChangButtonPrompt(MaskState TargetMaskState)
    {
        instance.indicator.SetActive(true);
    }

    public static void HideMaskChangButtonPrompt()
    {
        instance.indicator.SetActive(false);
    }
}
