using UnityEngine;
using Random = UnityEngine.Random;

public class NPCTheater : NPCCharacter
{
    // Methods

    protected override void InitChar()
    {
        cliqueKind = CliqueKind.THEATER;
    }

    protected override void InitTransform()
    {
        // Correct to the side to center movement

        transform.position += transform.right * npcSettings.theaterMoveSpeed * npcSettings.theaterSwitchTime / 2;

        velocity = DirSign() * transform.right * npcSettings.theaterMoveSpeed;
    }

	protected override void UpdateMovement()
    {
        velocity = DirSign() * transform.right * npcSettings.theaterMoveSpeed;
    }

    float DirSign()
    {
        float dTSpawn = Time.time - spawnTime;
        return (Mathf.FloorToInt(dTSpawn / npcSettings.theaterSwitchTime) % 2 == 0) ? -1 : 1;
    }
}
