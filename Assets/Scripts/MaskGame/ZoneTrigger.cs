using MaskGame.Character;
using UnityEngine;
namespace MaskGame
{
    public class ZoneTrigger : MonoBehaviour
    {
        public MaskState NewMaskState;

        public void OnTriggerEnter(Collider other)
        {
            PlayerCharacter c = other.GetComponentInParent<PlayerCharacter>();
            if (c != null)
            {
                c.RequestMaskStateChange(NewMaskState);
            }
        }

        public void OnTriggerExit(Collider other)
        {
            PlayerCharacter c = other.GetComponentInParent<PlayerCharacter>();
            if (c != null)
            {
                c.RequestMaskStateChange(MaskState.NONE);
            }
        }
    }
}