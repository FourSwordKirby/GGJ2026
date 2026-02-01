using UnityEngine;
using MaskGame.Character;

[CreateAssetMenu(fileName = "ReferenceLibrary", menuName = "ScriptableObjects/ReferenceLibrary")]
public class ReferenceLibrary : ScriptableObject
{
    public GameObject npcJockPrefab;
    public GameObject npcCheerPrefab;
    public GameObject npcBusinessPrefab;
    public GameObject npcNerdPrefab;
    public GameObject npcTheaterPrefab;

    public GameObject PrefabForClique(MaskState maskClique)
    {
        switch (maskClique)
        {
            case MaskState.JOCK: return npcJockPrefab;
            case MaskState.CHEER: return npcCheerPrefab;
            case MaskState.BUSINESS: return npcBusinessPrefab;
            case MaskState.NERD: return npcNerdPrefab;
            case MaskState.THEATER: return npcTheaterPrefab;
            default: return null;
        }
    }
}
