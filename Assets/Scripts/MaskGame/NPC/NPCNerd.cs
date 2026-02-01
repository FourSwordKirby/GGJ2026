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
    private float nerdmsTime;

    private float moveDuration;
    private float degTarget;

    // Methods

    protected override void InitChar()
    {
        cliqueKind = CliqueKind.NERD;
    }

    protected override void InitTransform()
    {
        // Randomize orientation, init tracking variables

        transform.Rotate(Vector3.up, Random.value * 360.0f);

        if (Random.value < 0.5f)
        {
            SetNerdMoveState(NerdMoveState.MOVING);
        }
        else
        {
            SetNerdMoveState(NerdMoveState.ROTATING);
        }
    }

    protected override void UpdateMovement()
    {
        UpdateNerdMoveState();
    }

    void UpdateNerdMoveState()
    {
        switch (nerdms)
        {
            case NerdMoveState.MOVING:
                {
                    // If going out of bounds, trigger a new path

                    Vector3 posProjected = transform.position + velocity * 1.05f * Time.deltaTime;

                    if (!owningZone.isPosWithinBounds(posProjected))
                    {
                        SetNerdMoveState(NerdMoveState.ROTATING);
                        break;
                    }

                    // If timer is up, trigger a new path

                    if (Time.time - nerdmsTime > moveDuration)
                    {
                        SetNerdMoveState(NerdMoveState.ROTATING);
                        break;
                    }
                }
                break;

            case NerdMoveState.ROTATING:
                {
                    // Update rotation

                    float degCur = transform.eulerAngles.y;

                    float angularSpeed = npcSettings.nerdRotationSpeed;
                    float maxStep = angularSpeed * Time.deltaTime;

                    float dDeg = Mathf.DeltaAngle(degCur, degTarget);

                    if (Mathf.Abs(dDeg) <= maxStep)
                    {
                        transform.rotation = Quaternion.Euler(0, degTarget, 0);

                        // Complete, start moving

                        SetNerdMoveState(NerdMoveState.MOVING);
                    }
                    else
                    {
                        float step = Mathf.Sign(dDeg) * maxStep;
                        transform.Rotate(0, step, 0);
                    }
                }
                break;
        }
    }

    void SetNerdMoveState(NerdMoveState nerdmsNext, bool avoidOutOfBounds = true)
    {
        if (nerdms == nerdmsNext)
            return;

        // Leave old state

        switch (nerdms)
        {
            case NerdMoveState.MOVING:
                {
                    velocity = Vector2.zero;
                }
                break;
        }

        // Enter new state

        nerdms = nerdmsNext;
        nerdmsTime = Time.time;

        switch (nerdms)
        {
            case NerdMoveState.MOVING:
                {
                    // Set duration

                    Vector2 range = npcSettings.nerdMarchTimeRange;
                    moveDuration = Random.Range(range.x, range.y);

                    velocity = transform.forward * npcSettings.nerdWalkSpeed;
                }
                break;

            case NerdMoveState.ROTATING:
                {
                    // Select new rotation

                    float rotationMax = npcSettings.nerdRotationMax;
                    float dDeg = Random.Range(-rotationMax, rotationMax);

                    if (avoidOutOfBounds)
                    {
                        // If going out of bounds, use bounds that turn us around

                        int sign = Random.Range(0, 2) * 2 - 1;
                        dDeg = sign * Random.Range(150, 180);
                    }

                    degTarget = Mathf.Repeat(transform.eulerAngles.y + dDeg + 180.0f, 360.0f) - 180.0f;
                }
                break;
        }
    }
}
