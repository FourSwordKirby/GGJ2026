using UnityEngine;
using Random = UnityEngine.Random;
using MaskGame.Character;

public class NPCTheater : NPCCharacter
{
    // Methods

    protected override void InitChar()
    {
        maskClique = MaskState.THEATER;
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
