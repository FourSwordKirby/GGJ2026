using MaskGame.Character;
using System.Net;
using UnityEngine;

public class StraightGuy : MonoBehaviour
{
    public float speed = 3f;
    public float knockbackStrength = 10f;
    public float maxDistance = 50f;
    public float maxLifetime = 20f;

    private Vector3 spawnPoint;
    public Vector3 moveDirection;
    private float timer = 0f;

    public void Start()
    {
        spawnPoint = transform.position;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    void Update()
    {
        var startPoint = transform.position;
        transform.position += moveDirection * speed * Time.deltaTime;
        var endPoint = transform.position;
        timer += Time.deltaTime;

        if (Vector3.Distance(spawnPoint, transform.position) >= maxDistance || timer >= maxLifetime)
        {
            Destroy(gameObject);
        }
        var direction = (endPoint - startPoint).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndBox"))
        {
            Destroy(gameObject);
        }

        if (other.TryGetComponent<PlayerCharacter>(out var player))
        {
            Vector3 impulseDir = (other.transform.position - transform.position).normalized;
            player.ExtendedRigidbody.ApplyImpulse(impulseDir * knockbackStrength, false);
        }
    }
}