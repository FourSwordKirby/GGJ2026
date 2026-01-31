using MaskGame.Character.Modifier;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MaskGame.Character
{
    public class PlayerCharacter : MaskGameCharacter
    {
        public MovementModifier Movement;

        protected CharacterInputs InputsForNextFixedUpdate;

        public MaskState RequestedMaskState = MaskState.NONE;
        private MaskState NextMaskState;
        public MaskState CurrentMaskState = MaskState.BASIC;

        protected override void Start()
        {
            base.Start();

            Movement = new WalkingMovementModifier(this);
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
            InputsForNextFixedUpdate.MovementIntention.z = direction.y;

            if (InputSystem.actions.FindAction("Toggle").IsPressed())
            {
                NextMaskState = RequestedMaskState;
            }
        }

        public override void PrePhysics(float deltaTime)
        {
            Movement.ApplyInputToCharacter(InputsForNextFixedUpdate, deltaTime);

            InputsForNextFixedUpdate = new CharacterInputs();
        }

        public override void PostPhysics(float deltaTime)
        {
            UpdateMaskState();
        }

        public void UpdateMaskState()
        {
            if (NextMaskState == MaskState.NONE)
            {
                return;
            }

            CurrentMaskState = NextMaskState;
            NextMaskState = MaskState.NONE;

            switch (CurrentMaskState)
            {
                case MaskState.JOCK:
                    foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                    {
                        m.materials[0].color = Color.red;
                    }
                    break;
                case MaskState.BASIC:
                default:
                    foreach (MeshRenderer m in GetComponentsInChildren<MeshRenderer>())
                    {
                        m.materials[0].color = Color.white;
                    }
                    break;
            }
        }

        public void RequestMaskStateChange(MaskState maskState)
        {
            RequestedMaskState = maskState;
        }
    }
}