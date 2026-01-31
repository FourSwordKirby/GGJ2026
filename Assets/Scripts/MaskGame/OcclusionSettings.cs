using UnityEngine;

[ExecuteAlways]
public class OcclusionSettings : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Min(0f)] private float cutoutRadius = 1.2f;
    [SerializeField] [Min(0f)] private float cutoutFeatherDist = 0.5f;

    void OnEnable()
    {
        ApplySettings();
    }

    void OnValidate()
    {
        ApplySettings();
    }

    void ApplySettings()
    {
        Shader.SetGlobalFloat("_CutoutRadius", cutoutRadius);
        Shader.SetGlobalFloat("_CutoutFeatherDist", cutoutFeatherDist);
    }
}