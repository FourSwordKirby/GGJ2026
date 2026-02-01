using UnityEngine;
using UnityEngine.InputSystem;

public class InputGuide : MonoBehaviour
{
    private Transform keyGroup;
    private Transform buttonGroup;

    void Awake()
    {
        keyGroup = transform.Find("Key");
        buttonGroup = transform.Find("Button");
    }

    private void OnEnable()
    {
        RefreshScheme(InputSchemeDetector.CurrentScheme);

        InputSchemeDetector.OnSchemeChanged += RefreshScheme;
    }

    private void OnDisable()
    {
        InputSchemeDetector.OnSchemeChanged -= RefreshScheme;
    }

    public void RefreshScheme(ControlSchemeType controlScheme)
    {
        bool keyboard = controlScheme == ControlSchemeType.Keyboard;
        keyGroup.gameObject.SetActive(keyboard);
        buttonGroup.gameObject.SetActive(!keyboard);
    }

}