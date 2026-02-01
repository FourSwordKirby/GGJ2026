using MaskGame.Character;
using UnityEngine;

public class StraightGuy : MonoBehaviour
{
    public float speed = 3f;
    public float knockbackStrength = 10f;
    public float maxDistance = 50f;
    public float maxLifetime = 20f;

    private Vector3 spawnPoint;
    private Vector3 moveDirection;
    private float timer = 0f;

    public void Initialize(Vector3 direction)
    {
        moveDirection = direction.normalized;
        spawnPoint = transform.position;

        if (moveDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }

    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
        timer += Time.deltaTime;

        if (Vector3.Distance(spawnPoint, transform.position) >= maxDistance || timer >= maxLifetime)
        {
            Destroy(gameObject);
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