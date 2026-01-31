using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject timerText;
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
        var tmp = timerText.GetComponent<TextMeshProUGUI>();
        tmp.text = ((int)gameManager.TimeRemaining).ToString();
    }
}
