using MaskGame.Character;
using TMPro;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PromptUI : MonoBehaviour
{
    public Animator promptAnimator;
    public TextMeshProUGUI promptText;
    public GameObject Alert;
    public bool ShowAlert = false;

    public static PromptUI instance;

    public void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public void Update()
    {
        if (ShowAlert != Alert.activeSelf)
        {
            Alert.SetActive(ShowAlert);
        }
        ShowAlert = false;
    }

    public static void ShowPrompt(string text)
    {
        instance.promptText.text = text;
        instance.promptAnimator.SetTrigger("Show");
    }

    public static void ShowMaskChangButtonPrompt(MaskState TargetMaskState)
    {
        //instance.indicator.SetActive(true);
    }

    public static void HideMaskChangButtonPrompt()
    {
        //instance.indicator.SetActive(false);
    }
}
