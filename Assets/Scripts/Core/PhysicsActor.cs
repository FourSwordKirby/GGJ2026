using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// A template actor behavior which is physics enabled.
    /// </summary>
    [RequireComponent(typeof(ExtendedRigidbody))]
    public class PhysicsActor : FixedUpdateActor
    {
        [Header(nameof(PhysicsActor) + " References")]
        public ExtendedRigidbody ExtendedRigidbody;

        protected override void OnValidate()
        {
            base.OnValidate();
            ExtendedRigidbody = GetComponent<ExtendedRigidbody>();
        }
    }
}