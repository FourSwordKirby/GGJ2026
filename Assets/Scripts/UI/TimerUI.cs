using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TextMeshPro timerText;

    // Update is called once per frame
    void Update()
    {
        string remaingTime = GameManager.instance.TimeRemaining.ToString("00");
        timerText.text = $"Class Starts in: {remaingTime}";
    }
}
