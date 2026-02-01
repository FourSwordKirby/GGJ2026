using MaskGame.Character;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject indicator;

    // singleton design pattern
    public static UIManager instance;

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
