using System;

namespace MaskGame.Character.Modifier
{
    [Serializable]
    public class MovementModifier
    {
        public MovementModifier()
        {
        }

        public virtual void ApplyInputToCharacter(MaskGameCharacter character, CharacterInputs inputs, float deltaTime)
        {

        }
    }
}