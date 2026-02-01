using MaskGame.Character.Modifier;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaskGame.Character
{
    [RequireComponent(typeof(PlayerMaskManager))]
    public class PlayerCharacter : MaskGameCharacter
    {
        public PlayerMaskManager MaskManager { get; private set; }

        protected CharacterInputs InputsForNextFixedUpdate;

        protected List<ZoneTrigger> OverlappedZones = new List<ZoneTrigger>();
        protected ZoneTrigger CurrentZone;

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
            HandleZones();
            MaskManager.Step(deltaTime);
            MaskManager.GetCurrentMovementMondifier().ApplyInputToCharacter(this, InputsForNextFixedUpdate, deltaTime);

            InputsForNextFixedUpdate.TriggerToggle = false;
            ExtendedRigidbody.ApplyGravityImpulse(deltaTime);
        }

        protected void HandleZones()
        {
            if (OverlappedZones.Count == 0)
            {
                CurrentZone = null;
            }
            else
            {
                // if we're re-assigning the current zone, we're entering a new zone and need to play the appropraite sound effect
                if (CurrentZone != OverlappedZones[0])
                    AudioManager.instance.PlayZoneEnter(OverlappedZones[0].ZoneMaskState);

                CurrentZone = OverlappedZones[0];
            }

            HandleInputsForZoneOverlaps();
            OverlappedZones.Clear();
        }

        protected void HandleInputsForZoneOverlaps()
        {
            if (CurrentZone != null)
            {
                // Find/Get UI Manager and request button prompt UI

                if (InputsForNextFixedUpdate.TriggerToggle)
                {
                    AudioManager.instance.PlayMaskSwitch();
                    MaskManager.QueueNextMaskState(CurrentZone.ZoneMaskState);
                }
            }
        }

        public override void PostPhysics(float deltaTime)
        {
        }

        public void RegisterZoneOverlap(ZoneTrigger zone)
        {
            OverlappedZones.Add(zone);
        }

        public bool IsInMismatchedZone()
        {
            if (CurrentZone)
            {
                return MaskManager.CurrentMaskState != CurrentZone.ZoneMaskState;
            }

            return false;
        }
    }
}