using MaskGame.Character;
using UnityEngine;

public class NpcBounceEffect : MonoBehaviour
{
    public float MaxImpulse = 1;
    public float DepenetrationRate = 0.5f;
    public float CharacterRadius = 0.5f;

    private void OnTriggerStay(Collider other)
    {
        PlayerCharacter player = other.GetComponentInParent<PlayerCharacter>();
        if (player == null)
        {
            return;
        }

        Vector3 delta = Vector3.ProjectOnPlane(player.ExtendedRigidbody.Position - transform.position, Vector3.up).normalized;
        delta *= 2 * CharacterRadius;
        Vector3 requiredVelocity = delta / Time.deltaTime;
        Vector3 impulse = requiredVelocity - player.ExtendedRigidbody.Velocity;
        impulse = Vector3.ClampMagnitude(impulse * DepenetrationRate, MaxImpulse);
        player.ExtendedRigidbody.ApplyImpulse(impulse, false);
    }
}
