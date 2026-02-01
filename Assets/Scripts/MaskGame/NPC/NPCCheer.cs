using UnityEngine;
using Random = UnityEngine.Random;
using MaskGame.Character;

public class NPCCheer : NPCCharacter
{
    // Methods

    protected override void InitChar()
    {
        maskClique = MaskState.CHEER;
    }

    protected override void InitTransform()
    {
        // Start in right hop position

        transform.position += Vector3.right * (npcSettings.cheerHopDist / 2.0f);
    }

    protected override void UpdateMovement()
    {
        // Hop, hop, left, hop, hop, right

        // float dTFromTime = cheerHopTime - Time.time;
    }

    // Cheerleader

    enum HopKind
    {
        NEUTRAL,
        LEFT,
        RIGHT
    }

    void Hop(HopKind HopKind)
    {

    }
}
