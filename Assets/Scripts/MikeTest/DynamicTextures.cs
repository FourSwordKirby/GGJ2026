using UnityEditor;
using UnityEngine;

public class DynamicTextures : MonoBehaviour
{
    private Renderer cubeRenderer;
    public float cycleSpeed = 0.1f; 

    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        if (cubeRenderer == null)
        {
            Debug.LogError("Cube does not have a Renderer component!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        float hue = (Time.time * cycleSpeed) % 1f;
        
        Color rainbowColor = Color.HSVToRGB(hue, 1f, 1f);

        cubeRenderer.material.color = rainbowColor;
    }
}
