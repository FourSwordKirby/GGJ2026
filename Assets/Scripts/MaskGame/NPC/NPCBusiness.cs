using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using MaskGame.Character;

public class NPCBusiness : NPCCharacter
{
    enum DirectionKind
    {
        FORWARD,
        BACK,
        LEFT,
        RIGHT,
        MAX,
        MIN = FORWARD,
        NIL = -1
    }

    DirectionKind direction = DirectionKind.NIL;
    private float dirTime;

    private float dirDuration;

    // Methods

    protected override void InitChar()
    {
        maskClique = MaskState.BUSINESS;

        base.InitChar();
    }

    protected override void InitTransform()
    {
        // Randomize into one of four cardinals

        DirectionKind dir = (DirectionKind)Random.Range((int)DirectionKind.MIN, (int)DirectionKind.MAX);
        SetDirection(dir);
    }

    protected override void UpdateMovement()
    {
        // Switch if going out of bounds

        Vector3 posProjected = transform.position + velocity * Time.deltaTime;
        if (!owningZone.isPosWithinBounds(posProjected))
        {
            SetDirection(DirOpposite(direction));
            return;
        }

        // Switch if time is up

        if (Time.time - dirTime > dirDuration)
        {
            SwitchDirection();
            return;
        }
    }

    Vector3 VecFromDir(DirectionKind dir)
    {
        switch (dir)
        {
            case DirectionKind.FORWARD: return Vector3.forward;
            case DirectionKind.BACK: return Vector3.back;
            case DirectionKind.LEFT: return Vector3.left;
            case DirectionKind.RIGHT: return Vector3.right;
            default: return Vector3.forward;
        }
    }

    DirectionKind DirOpposite(DirectionKind dir)
    {
        switch (dir)
        {
            case DirectionKind.FORWARD: return DirectionKind.BACK;
            case DirectionKind.BACK: return DirectionKind.FORWARD;
            case DirectionKind.LEFT: return DirectionKind.RIGHT;
            case DirectionKind.RIGHT: return DirectionKind.LEFT;
            default: return DirectionKind.FORWARD;
        }
    }

    void SwitchDirection()
    {
        // Random excluding current

        List<DirectionKind> dirList = new List<DirectionKind>();

        for (DirectionKind dir = DirectionKind.MIN; dir < DirectionKind.MAX; dir++)
        {
            if (dir == direction)
                continue;

            dirList.Add(dir);
        }

        SetDirection(dirList[Random.Range(0, dirList.Count)]);
    }

    void SetDirection(DirectionKind dirNext)
    {
        if (direction == dirNext)
            return;

        direction = dirNext;
        dirTime = Time.time;

        transform.rotation = Quaternion.LookRotation(VecFromDir(direction));
        velocity = -transform.forward * npcSettings.businessWalkSpeed;

        Vector2 range = npcSettings.businessMarchTimeRange;
        dirDuration = Random.Range(range.x, range.y);
    }
}
