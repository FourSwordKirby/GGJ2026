using MaskGame.Character;
using UnityEngine;
namespace MaskGame
{
    public class ZoneTrigger : MonoBehaviour
    {
        public MaskState NewMaskState;

        public void OnTriggerStay(Collider other)
        {
            PlayerCharacter c = other.GetComponentInParent<PlayerCharacter>();
            if (c != null)
            {
                c.RegisterZoneOverlap(this);
            }
        }
    }
}