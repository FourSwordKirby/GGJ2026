using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class NPCZone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private CliqueKind cliqueKind;

    [Header("References")]
    [SerializeField] private NPCSettings npcSettings;
    [SerializeField] private ReferenceLibrary referenceLibrary;

    private Rect rectBounds;
    private Transform spawnGroup;

    void Start()
    {
        spawnGroup = (new GameObject("NPCZoneSpawnGroup")).transform;
        RefreshRectBounds();
        SpawnCrowd();
    }

    void SpawnCrowd()
    {
        switch (cliqueKind)
        {
            case CliqueKind.JOCK:
                {
                    SpawnNPCGrid(unitSize: 4, offsetLimit: 1);
                }
                break;

            case CliqueKind.CHEERLEADER:
                {
                    SpawnNPCGrid(unitSize: 5);

                }
                break;

            case CliqueKind.BUSINESS:
                {
                    SpawnNPCGrid(unitSize: 4, populationRatio: 0.6f);
                }
                break;

            case CliqueKind.NERD:
                {
                    SpawnNPCGrid(unitSize: 4, populationRatio: 0.3f);
                }
                break;

            case CliqueKind.THEATER:
                {
                    float marginHori = npcSettings.theaterMoveSpeed * npcSettings.theaterSwitchTime * 1.05f;
                    SpawnNPCGrid(unitSize: 3, offsetLimit: 0.5f, marginHori: marginHori, marginVert: 3);
                }
                break;
        }
    }

    void SpawnNPCGrid(
        float unitSize = 1,
        float offsetLimit = 0,
        float populationRatio = 1.0f,
        float marginHori = 1.0f,
        float marginVert = 1.0f)
    {
        // Get spawn points

        float width = rectBounds.width - (marginHori * 2);
        float height = rectBounds.height - (marginVert * 2);
        float xMin = transform.position.x - (width / 2) + (unitSize / 2);
        float zMin = transform.position.z - (height / 2) + (unitSize / 2);
        Vector3 spawnMin = new Vector3(xMin, transform.position.y, zMin);

        int rowCount = Mathf.FloorToInt(width / unitSize);
        int colCount = Mathf.FloorToInt(height / unitSize);

        int totalCount = rowCount * colCount;
        int pullCount = Mathf.RoundToInt(populationRatio * totalCount);

        List<int> spawnIndices = PullFromShuffledDeck(pullCount, 0, totalCount);

        // Spawn at points

        foreach (int spawnIndex in spawnIndices)
        {
            int rowIndex = spawnIndex / rowCount;
            int colIndex = spawnIndex % rowCount;

            Vector2 dPosSpawn = Vector2.right * unitSize * colIndex + Vector2.up * unitSize * rowIndex;
            dPosSpawn += Random.insideUnitCircle * offsetLimit;

            float clampedX = Mathf.Clamp(dPosSpawn.x, 0, width);
            float clampedY = Mathf.Clamp(dPosSpawn.y, 0, height);

            dPosSpawn = new Vector2(clampedX, clampedY);

            Vector3 spawnPos = spawnMin + new Vector3(dPosSpawn.x, 0, dPosSpawn.y);

            GameObject npcObj = Instantiate(referenceLibrary.PrefabForClique(cliqueKind), spawnPos, Quaternion.identity, spawnGroup);
            NPCCharacter npcChar = npcObj.GetComponent<NPCCharacter>();
            npcChar.Init(this);
        }
    }

    void RefreshRectBounds()
    {
        Vector3 vecScale = transform.localScale;
        float xMin = transform.position.x - (vecScale.x / 2);
        float zMin = transform.position.z - (vecScale.z / 2);
        this.rectBounds = new Rect(xMin, zMin, vecScale.x, vecScale.z);
    }

    public bool isPosWithinBounds(Vector3 worldPos)
    {
        Vector2 vecXZ = new Vector2(worldPos.x, worldPos.z);
        return rectBounds.Contains(vecXZ);
    }

    // Gizmos

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.5f);
        Gizmos.DrawCube(transform.position, transform.localScale);
    }

    // Util

    List<int> PullFromShuffledDeck(int pullCount, int rangeMin, int rangeMax)
    {
        List<int> deck = new List<int>(rangeMax - rangeMin);
        for (int i = rangeMin; i < rangeMax; i++)
        {
            deck.Add(i);
        }

        List<int> result = new List<int>();
        for (int i = 0; i < pullCount; i++)
        {
            int randomIndex = Random.Range(0, deck.Count);
            result.Add(deck[randomIndex]);

            deck.RemoveAt(randomIndex);
        }
        return result;
    }
}
