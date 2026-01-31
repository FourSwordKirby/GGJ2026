using UnityEngine;

[CreateAssetMenu(fileName = "ReferenceLibrary", menuName = "ScriptableObjects/ReferenceLibrary")]
public class ReferenceLibrary : ScriptableObject
{
    public GameObject npcJockPrefab;
    public GameObject npcCheerPrefab;
    public GameObject npcBusinessPrefab;
    public GameObject npcNerdPrefab;
    public GameObject npcTheaterPrefab;

    public GameObject PrefabForClique(CliqueKind cliqueKind)
    {
        switch (cliqueKind)
        {
            case CliqueKind.JOCK: return npcJockPrefab;
            case CliqueKind.CHEERLEADER: return npcCheerPrefab;
            case CliqueKind.BUSINESS: return npcBusinessPrefab;
            case CliqueKind.NERD: return npcNerdPrefab;
            case CliqueKind.THEATER: return npcTheaterPrefab;
            default: return null;
        }
    }
}
