using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject indicator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowButtonPrompt()
    {
        indicator.SetActive(true);
    }

    public void HideButtonPrompt()
    {
        indicator.SetActive(false);
    }
}
