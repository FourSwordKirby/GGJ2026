using MaskGame.Character.Modifier;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaskGame.Character
{
    [RequireComponent(typeof(PlayerMaskManager))]
    public class PlayerCharacter : MaskGameCharacter
    {
        public PlayerMaskManager MaskManager { get; private set; }

        protected CharacterInputs InputsForNextFixedUpdate;

        protected ZoneTrigger OverlappedZone;

        protected override void OnValidate()
        {
            base.OnValidate();
            MaskManager = GetComponent<PlayerMaskManager>();
        }

        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected void Update()
        {
            ReadInputs();
        }

        protected void ReadInputs()
        {
            // InputsForNextFixedUpdate.MovementIntention = Vector3.zero;
            Vector2 direction = InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
            InputsForNextFixedUpdate.MovementIntention.x = direction.x;
            InputsForNextFixedUpdate.MovementIntention.y = 0;
            InputsForNextFixedUpdate.MovementIntention.z = direction.y;

            InputsForNextFixedUpdate.TriggerToggle = InputsForNextFixedUpdate.TriggerToggle || InputSystem.actions.FindAction("Toggle").IsPressed();
        }

        public override void PrePhysics(float deltaTime)
        {
            HandleInputsForZoneOverlaps();
            MaskManager.Step(deltaTime);
            MaskManager.GetCurrentMovementMondifier().ApplyInputToCharacter(this, InputsForNextFixedUpdate, deltaTime);

            InputsForNextFixedUpdate = new CharacterInputs();
        }

        protected void HandleInputsForZoneOverlaps()
        {
            if (OverlappedZone != null)
            {
                // Find/Get UI Manager and request button prompt UI

                if (InputsForNextFixedUpdate.TriggerToggle)
                {
                    MaskManager.QueueNextMaskState(OverlappedZone.NewMaskState);
                }
            }
            OverlappedZone = null;
        }

        public override void PostPhysics(float deltaTime)
        {
        }

        public void RegisterZoneOverlap(ZoneTrigger zone)
        {
            OverlappedZone = zone;
        }
    }
}