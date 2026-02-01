using UnityEngine;
using Random = UnityEngine.Random;
using MaskGame.Character;
using MaskGame.Cheerleader;

public class NPCCheer : NPCCharacter
{
    protected CheerleaderManager CheerManager;

    private Vector3 centerPos;
    private Vector3 neutralHopPos;

    const float HOP_TIME = 0.3f;
    const float HOP_HEIGHT_RATIO = 0.7f;

    enum HopKind
    {
        NEUTRAL,
        LEFT,
        RIGHT,
        NIL = -1
    }

    private HopKind hopKind = HopKind.NIL;
    private float hopTime;

    // Methods

    protected override void InitChar()
    {
        maskClique = MaskState.CHEER;

        if (CheerManager == null)
        {
            CheerManager = GameObject.FindAnyObjectByType<CheerleaderManager>();
        }

        if (CheerManager != null)
        {
            CheerManager.RegisterBeatCallback(this, HandleBeatCallback);
        }
    }

    protected override void InitTransform()
    {
        // Start in right hop position

        centerPos = transform.position;
        transform.position = GetEndPosition(HopKind.RIGHT);
    }

    protected override void UpdateMovement()
    {
        UpdateHop();
    }

    void SetHop(HopKind hopKindNext)
    {
        // Don't early return on repeated hops

        // Leave old state - snap to end

        switch (hopKind)
        {
            case HopKind.RIGHT:
            case HopKind.LEFT:
            case HopKind.NEUTRAL:
                {
                    transform.position = GetEndPosition(hopKind);
                    transform.rotation = Quaternion.identity;
                }
                break;
        }

        // Enter new state

        hopKind = hopKindNext;
        hopTime = Time.time;

        switch (hopKind)
        {
            case HopKind.NEUTRAL:
                {
                    neutralHopPos = transform.position;
                }
                break;
        }

        // Update immediately to get into position

        UpdateHop();
    }

    void UpdateHop()
    {
        if (hopKind == HopKind.NIL)
            return;

        float dT = Time.time - hopTime;

        if (dT > HOP_TIME)
        {
            // Snap to complete

            SetHop(HopKind.NIL);
            return;
        }

        // Lerp a jump

        transform.position = QuadraticBezier(
            GetStartPosition(hopKind),
            GetMidPosition(hopKind),
            GetEndPosition(hopKind),
            dT / HOP_TIME
        );

        // Lerp a spin

        if (hopKind != HopKind.NEUTRAL)
        {
            int sign = (hopKind == HopKind.LEFT) ? -1 : 1;
            float angle = sign * 360 * (dT / HOP_TIME);
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
        }
    }

    float HopHeight()
    {
        return npcSettings.cheerHopDist * HOP_HEIGHT_RATIO;
    }

    Vector3 QuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        Vector3 a = Vector3.Lerp(p0, p1, t);
        Vector3 b = Vector3.Lerp(p1, p2, t);
        return Vector3.Lerp(a, b, t);
    }

    void HandleBeatCallback(CheerBeatCallbackInput input)
    {
        HopKind hopKind = HopKind.NEUTRAL;

        if (input.IsMajorBeat)
        {
            hopKind = (input.IsLeft) ? HopKind.LEFT : HopKind.RIGHT;
        }

        SetHop(hopKind);
    }

    Vector3 GetDisplacement()
    {
        return Vector3.right * (npcSettings.cheerHopDist / 2.0f);
    }

    Vector3 GetStartPosition(HopKind hopKind)
    {
        switch (hopKind)
        {
            case HopKind.LEFT: return centerPos + GetDisplacement();
            case HopKind.RIGHT: return centerPos - GetDisplacement();
            case HopKind.NEUTRAL: return neutralHopPos;
        }

        return Vector3.zero;
    }

    Vector3 GetEndPosition(HopKind hopKind)
    {
        switch (hopKind)
        {
            case HopKind.LEFT: return centerPos - GetDisplacement();
            case HopKind.RIGHT: return centerPos + GetDisplacement();
            case HopKind.NEUTRAL: return neutralHopPos;
        }

        return Vector3.zero;
    }

    Vector3 GetMidPosition(HopKind hopKind)
    {
        switch (hopKind)
        {
            case HopKind.LEFT: return centerPos + Vector3.up * HopHeight();
            case HopKind.RIGHT: return centerPos + Vector3.up * HopHeight();
            case HopKind.NEUTRAL: return neutralHopPos + Vector3.up * HopHeight();
        }

        return Vector3.zero;
    }
}
