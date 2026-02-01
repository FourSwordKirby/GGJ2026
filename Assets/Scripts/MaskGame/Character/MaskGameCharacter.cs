using UnityEngine;
using Core;
using System;

namespace MaskGame.Character
{
    public class MaskGameCharacter : PhysicsActor
    {
        public override void PrePhysics(float deltaTime)
        {
            ExtendedRigidbody.ApplyGravityImpulse(deltaTime);
        }


        public Vector3 GetFacingDirection()
        {
            return ExtendedRigidbody.transform.forward;
        }

        public void SetFacingDirection(Vector3 direction)
        {
            if (direction.sqrMagnitude < 0.1)
            {
                return;
            }
            direction.Normalize();
            Quaternion rot = Quaternion.LookRotation(direction, Vector3.up);
            ExtendedRigidbody.SetBodyRotation(rot);
        }
    }
}