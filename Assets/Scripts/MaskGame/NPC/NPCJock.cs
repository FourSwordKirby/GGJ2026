using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using MaskGame.Character;

public class NPCJock : NPCCharacter
{
    private Vector3 posPivot;

    // Methods

    protected override void InitChar()
    {
        maskClique = MaskState.JOCK;
    }

    protected override void InitTransform()
    {
        // Use spawn position as pivot

        posPivot = transform.position;

        // Create distance from pivot

        transform.Rotate(Vector3.up, Random.value * 360.0f);
        transform.position += transform.right * npcSettings.jockRunRadius;
    }

	protected override void UpdateMovement()
    {
        // Ignore velocity, use angular math instead

        float angularSpeed = -npcSettings.jockRunVelocity * Mathf.Rad2Deg / npcSettings.jockRunRadius;
        transform.RotateAround(posPivot, Vector3.up, angularSpeed * Time.deltaTime);
    }
}
