using UnityEngine;
using Random = UnityEngine.Random;

public class NPCNerd : NPCCharacter
{
    private enum NerdMoveState
    {
        MOVING,
        ROTATING,
        NIL = -1
    }

    private NerdMoveState nerdms = NerdMoveState.NIL;

    private float nerdMoveDuration;
    private float nerdTargetRotation;

    // Methods

    protected override void InitChar()
    {
        cliqueKind = CliqueKind.NERD;
    }

    protected override void InitTransform()
    {
        // Randomize orientation, init tracking variables

        transform.Rotate(Vector3.up, Random.value * 360.0f);
    }

    protected override void UpdateMovement()
    {
        UpdateNerdMoveState();
    }

    void UpdateNerdMoveState()
    {
        // if (isNerdMoving)
        // {
        //     // If going out of bounds, trigger a new path

        //     // If timer is up, trigger a new path
        // }
        // else
        // {
        //     // Update rotation

        //     // Start moving if rotation is complete
        // }
    }
}
