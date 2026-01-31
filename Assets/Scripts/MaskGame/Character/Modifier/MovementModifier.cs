using System;

namespace MaskGame.Character.Modifier
{
    [Serializable]
    public class MovementModifier
    {
        public MaskGameCharacter Character;

        public MovementModifier(MaskGameCharacter character)
        {
            Character = character;
        }

        public virtual void ApplyInputToCharacter(CharacterInputs inputs, float deltaTime)
        {

        }
    }
}