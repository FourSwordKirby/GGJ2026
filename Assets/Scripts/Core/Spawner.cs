using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject studentPrefab;
    public Vector3 travelDirection = Vector3.forward;
    public float minSpawnTime = 1f;
    public float maxSpawnTime = 5f;

    private BoxCollider spawnArea;

    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();
        StartCoroutine(SpawnRoutine());
    }

    public IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            Vector3 randomPoint = new Vector3(
                Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
                Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
                Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z)
            );

            GameObject studentObj = Instantiate(studentPrefab, randomPoint, Quaternion.identity);
            if (studentObj.TryGetComponent<StraightGuy>(out var student))
            {
                student.Initialize(travelDirection);
            }
        }
    }
}