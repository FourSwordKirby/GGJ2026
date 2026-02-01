using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    public TextMeshPro timerText;
    private GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var tmp = GameObject.Find("GameManager");
        if (tmp == null)
        {
            Debug.Log("GameManager not found in scene.");
        }
        else
        {

            gameManager = tmp.GetComponent<GameManager>();
        }
        if(gameManager == null)
        {
            Debug.Log("GameManager script not attached.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timerText.text = ((int)GameManager.instance.TimeRemaining).ToString();
    }
}
