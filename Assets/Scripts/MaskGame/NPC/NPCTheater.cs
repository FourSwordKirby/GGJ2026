using UnityEngine;
using Random = UnityEngine.Random;
using MaskGame.Character;
using MaskGame.Theater;

public class NPCTheater : NPCCharacter
{
    protected TheaterManager TheaterManager;

    private bool isLeft = true;
    private Vector3 posCenter;

    // Methods

    protected override void InitChar()
    {
        maskClique = MaskState.THEATER;

        if (TheaterManager == null)
        {
            TheaterManager = GameObject.FindAnyObjectByType<TheaterManager>();
        }

        if (TheaterManager != null)
        {
            TheaterManager.RegisterCallback(this, HandleCallback);
        }

        base.InitChar();
    }

    private void OnEnable()
    {
        // Skip if not initialized yet

        if (!TheaterManager)
            return;

        // We're coming back online from culling, snap to phase

        TheaterManager.PhaseCur(out bool isLeft, out float uTilSwitch);
        SnapToPhase(isLeft, uTilSwitch);
    }

    void SnapToPhase(bool isLeft, float uTilSwitch)
    {
        this.isLeft = isLeft;

        Vector3 dPos = transform.right * npcSettings.theaterMoveSpeed * TheaterManager.TimeBetweenBeatsSeconds / 2;
        float uValue = (isLeft) ? 1 - uTilSwitch : uTilSwitch;
        transform.position = Vector3.Lerp(posCenter - dPos, posCenter + dPos, uValue);
    }

    void OnDestroy()
    {
        TheaterManager?.RemoveCallback(this);
    }

    void HandleCallback(TheaterManager.CallbackInput input)
    {
        isLeft = input.IsLeft;
    }

    protected override void InitTransform()
    {
        posCenter = transform.position;

        TheaterManager.PhaseCur(out bool isLeft, out float uTilSwitch);
        SnapToPhase(isLeft, uTilSwitch);
    }

    protected override void UpdateMovement()
    {
        TheaterManager.PhaseCur(out bool isLeft, out float uTilSwitch);
        SnapToPhase(isLeft, uTilSwitch);
        animator.SetBool("left", isLeft);
    }
}
