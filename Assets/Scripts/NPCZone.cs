using Unity.VisualScripting;
using UnityEngine;

public class NPCZone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private CliqueKind cliqueKind;

    [Header("References")]
    [SerializeField] private NPCSettings npcSettings;
    [SerializeField] private ReferenceLibrary referenceLibrary;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnCrowd();
    }

    void SpawnCrowd()
    {
        switch (cliqueKind)
        {
            case CliqueKind.JOCK:
                {
                    SpawnNPCGrid(density: 20, noise: 0);
                }
                break;

            case CliqueKind.CHEERLEADER:
                {
                    SpawnNPCGrid(density: 20, noise: 0);

                }
                break;

            case CliqueKind.BUSINESS:
                {
                    SpawnNPCGrid(density: 20, noise: 0);

                }
                break;

            case CliqueKind.NERD:
                {
                    SpawnNPCGrid(density: 20, noise: 0);
                }
                break;

            case CliqueKind.THEATER:
                {
                    SpawnNPCGrid(density: 20, noise: 0);
                }
                break;
        }
    }

    void SpawnNPCGrid(float density, float noise)
    {
        // density = 
    }

    // Gizmos

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}
