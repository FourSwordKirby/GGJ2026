using Unity.VisualScripting;
using UnityEngine;

public class OcclusionManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject playerObj;

    void LateUpdate()
    {
        Camera cam = Camera.main;

        Vector3 viewPos = cam.worldToCameraMatrix.MultiplyPoint(playerObj.transform.position);

        Shader.SetGlobalVector("_PlayerWorldPos", playerObj.transform.position);
        Shader.SetGlobalVector("_PlayerViewPos", viewPos);
    }
}
