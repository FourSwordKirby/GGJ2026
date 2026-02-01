using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using MaskGame.Character;

public class NPCZone : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private MaskState maskClique;
    [SerializeField] private bool renderGizmo = false;

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

    void Update()
    {
        spawnGroup.gameObject.SetActive(FIsZoneInFrustum());
    }

    void SpawnCrowd()
    {
        switch (maskClique)
        {
            case MaskState.JOCK:
                {
                    SpawnNPCGrid(unitSize: 2, offsetLimit: 0.5f);
                }
                break;

            case MaskState.CHEER:
                {
                    float marginHori = npcSettings.cheerHopDist * 1.02f;
                    SpawnNPCGrid(unitSize: 2.5f, offsetLimit: 0.2f, marginHori: marginHori);

                }
                break;

            case MaskState.BUSINESS:
                {
                    SpawnNPCGrid(unitSize: 2, populationRatio: 0.8f);
                }
                break;

            case MaskState.NERD:
                {
                    SpawnNPCGrid(unitSize: 2, populationRatio: 0.8f);
                }
                break;

            case MaskState.THEATER:
                {
                    float marginHori = npcSettings.theaterMoveSpeed * npcSettings.theaterSwitchTime * 1.02f;
                    SpawnNPCGrid(unitSize: 1, offsetLimit: 0.3f, marginHori: marginHori, marginVert: 0.8f);
                }
                break;
        }
    }

    void SpawnNPCGrid(
        float unitSize = 0.5f,
        float offsetLimit = 0,
        float populationRatio = 1.0f,
        float marginHori = 0.5f,
        float marginVert = 0.5f)
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

            GameObject npcObj = Instantiate(referenceLibrary.PrefabForClique(maskClique), spawnPos, Quaternion.identity, spawnGroup);
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
        if (!renderGizmo)
            return;

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

    bool FIsZoneInFrustum()
    {
        Camera cam = Camera.main;

        Bounds bounds = new Bounds(transform.position, new Vector3(rectBounds.x, 2.0f, rectBounds.y));

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        bool visible = GeometryUtility.TestPlanesAABB(planes, bounds);

        return visible;
    }
}
