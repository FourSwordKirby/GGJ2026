using UnityEngine;
using UnityEngine.InputSystem;
using System;

public enum ControlSchemeType
{
    Keyboard,
    Gamepad
}

public class InputSchemeDetector : MonoBehaviour
{
    public InputActionAsset actions;

    public static ControlSchemeType CurrentScheme { get; private set; }

    public static event Action<ControlSchemeType> OnSchemeChanged;

    void OnEnable()
    {
        foreach (var action in actions)
        {
            action.performed += OnActionPerformed;
        }

        actions.Enable();
    }

    void OnDisable()
    {
        foreach (var action in actions)
        {
            action.performed -= OnActionPerformed;
        }
    }

    void OnActionPerformed(InputAction.CallbackContext ctx)
    {
        var device = ctx.control.device;

        if (device is Gamepad)
        {
            SetScheme(ControlSchemeType.Gamepad);
        }
        else if (device is Keyboard)
        {
            SetScheme(ControlSchemeType.Keyboard);
        }
    }

    void SetScheme(ControlSchemeType newScheme)
    {
        if (newScheme == CurrentScheme)
            return;

        CurrentScheme = newScheme;
        OnSchemeChanged?.Invoke(newScheme);

        Debug.Log("Control scheme switched to: " + newScheme);
    }
}