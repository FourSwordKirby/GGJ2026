using UnityEngine;
using Random = UnityEngine.Random;

public class NPCBusiness : NPCCharacter
{
    // Methods

    protected override void InitChar()
    {
        cliqueKind = CliqueKind.BUSINESS;
    }

    protected override void InitTransform()
    {
        // Randomize into one of four cardinals

        int signX = (Random.value < 0.5f) ? 1 : -1;
        int signZ = (Random.value < 0.5f) ? 1 : -1;
        Vector3 normalFace = new Vector3(signX, 0, signZ);
        transform.rotation = Quaternion.LookRotation(normalFace);

        velocity = -transform.forward * npcSettings.businessWalkSpeed;
    }

    protected override void UpdateMovement()
    {
        // Move back til edge

        Vector3 posProjected = transform.position + velocity * 1.05f * Time.deltaTime;
        owningZone.isPosWithinBounds(posProjected);
    }

    void SwitchDirection()
    {

    }
}
