using System;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody))]
    /// <summary>
    /// A ExtendedRigidBody component represents an extended interface for the Unity Rigidbody.
    /// It provides functions and routines for manipulating the Object's Physics representation through the scene.
    /// If a GameObject uses a Rigidbody or needs Physics, you should only manipulate the Physics through the Movement component.
    /// 
    /// While this might be annoying to do, code that directly access an object's Rigidbody is difficult to track,
    /// and can cause strange physics bugs when code in different places all try to manipulate an object's physics state.
    /// In particular, do not directly set the rigidbody velocity, use the SetBodyVelocty() or ApplyImpulse(), ApplyForce() functions instead.
    /// This makes it easier to put a breakpoint on velocity assignments.
    /// </summary>
    public class ExtendedRigidbody : MonoBehaviour
    {
        public enum TeleportMode
        {
            /// <summary>
            /// Moves the body from A to B with interpolated collisions betweeen.
            /// NOTE: Interpolation only works on Kinematic (or frozen) objects.
            /// </summary>
            Interpolate,

            /// <summary>
            /// Teleports the body from point A to B without performing any interpolated collision in between.
            /// </summary>
            Teleport,
        }

        /// <summary>
        /// Reference to Rigidbody. Do not allow external access. Not even in children classes.
        /// Everything should go through the public properties of Movement so that we can track their usage.
        /// </summary>
        [SerializeField] // Must serialize component references so that OnValidate can assign the value at edit-time.
        private Rigidbody Body;

        /// <summary>
        /// Read-only access to the current body velocity.
        /// </summary>
        public Vector3 Velocity => IsFrozen ? savedVelocity : Body.linearVelocity;

        /// <summary>
        /// Read-only access to the current body position.
        /// </summary>
        public Vector3 Position => Body.position;

        /// <summary>
        /// Read-only access to the current body rotation.
        /// </summary>
        public Quaternion Rotation => Body.rotation;

        /// <summary>
        /// Read-only access to the current inverse mass (1 / mass). Returns 0 if mass is 0.
        /// </summary>
        public float InverseMass => Math.Abs(Body.mass) < 0.0001f ? 0.0f : 1f / Body.mass;

        /// <summary>
        /// The Gravity Scale for this specific object, editable from Editor.
        /// </summary>
        public float GravityScale = 2.0f;

        // Global Gravity doesn't appear in the inspector. Modify it here in the code
        // (or via scripting) to define a different default gravity for all objects.
        public static float GlobalGravity = -9.81f;

        /// <summary>
        /// Returns true if Body has been Frozen (effectively IsKinematic)
        /// </summary>
        public bool IsFrozen { get; protected set; }

        /// <summary>
        /// If simulation should be enabled.
        /// </summary>
        public bool IsEnabled { get; protected set; }

        /// <summary>
        /// Set to true if physics writes should be logged to the console.
        /// </summary>
        public bool UseDebugLog = false;

        protected Vector3 savedVelocity = Vector3.zero;
        protected float savedGravityScale = 0.0f;

        public virtual void Awake()
        {

        }

        public virtual void OnValidate()
        {
            Body = GetComponent<Rigidbody>();
            Body.useGravity = false;
        }

        public virtual void ConfigureBody(
            bool isKinematic,
            float mass = 1.0f,
            float gravityScale = 2.0f)
        {
            Body.isKinematic = isKinematic;
            //Body.mass = mass;
            //GravityScale = gravityScale;
        }

        /// <summary>
        /// Apply an impulse from gravity. Flying objects can ignore calling this, or set GravityScale to 0.
        /// </summary>
        public virtual void ApplyGravityImpulse(float deltaTime)
        {
            Vector3 gravity = GlobalGravity * GravityScale * Vector3.up;
            ApplyForce(gravity, deltaTime, true);
        }

        /// <summary>
        /// Prevents any other command from working on this body.
        /// No commands will be queued.
        /// </summary>
        public virtual void Disable()
        {
            // TODO: unimplemented, waiting for use case
        }

        public virtual void Freeze()
        {
            if (!IsFrozen)
            {
                savedVelocity = Velocity;
                savedGravityScale = GravityScale;
            }

            Log($"Freeze(vel: {Velocity}, grav: {GravityScale}) => (savedVel: {savedVelocity}, savedGrav: {savedGravityScale})");

            SetBodyVelocity(Vector3.zero);

            IsFrozen = true;
            Body.isKinematic = true;
        }

        public virtual void Unfreeze()
        {
            Log($"Unfreeze() => (isFrozen: {IsFrozen}, savedVel: {savedVelocity}, savedGrav: {savedGravityScale})");

            if (!IsFrozen)
            {
                return;
            }

            if (IsFrozen)
            {
                GravityScale = savedGravityScale;
            }

            IsFrozen = false;
            Body.isKinematic = false;
            SetBodyVelocity(savedVelocity);
        }

        public void Unfreeze(Vector3 targetVelocity, float targetGravityScale)
        {
            Log($"Unfreeze(vel: {targetVelocity}, gravity: {targetGravityScale})");
            GravityScale = targetGravityScale;
            IsFrozen = false;
            Body.isKinematic = false;
            SetBodyVelocity(targetVelocity);
        }

        /// <summary>
        /// Unconditionally set the body position.
        /// Prefer setting the velocity instead of the position when possible.
        /// </summary>
        /// <param name="mode">determines if the body will interpolate from its old transform to its new transform. Interpolation only works if the body is in Kinematic mode.</param>
        public virtual void SetBodyPosition(Vector3 p, TeleportMode mode = TeleportMode.Interpolate)
        {
            Log($"SetBodyPosition (p: {p}, mode: {mode})");
            switch (mode)
            {
                case TeleportMode.Teleport:
                {
                    Body.position = p;
                    break;
                }
                case TeleportMode.Interpolate:
                default:
                {
                    Body.MovePosition(p);
                    break;
                }
            }
        }

        /// <summary>
        /// Unconditionally set the body rotation.
        /// </summary>
        /// <param name="mode">determines if the body will interpolate from its old transform to its new transform. Interpolation only works if the body is in Kinematic mode.</param>
        public virtual void SetBodyRotation(Quaternion r, TeleportMode mode = TeleportMode.Interpolate)
        {
            Log($"SetBodyRotation (r: {r}, mode: {mode})");
            switch (mode)
            {
                case TeleportMode.Teleport:
                {
                    Body.rotation = r;
                    break;
                }
                case TeleportMode.Interpolate:
                default:
                {
                    Body.MoveRotation(r);
                    break;
                }
            }
        }

        /// <summary>
        /// Unconditionally set the body transform.
        /// Prefer setting the velocity instead of the position when possible.
        /// </summary>
        /// <param name="mode">determines if the body will interpolate from its old transform to its new transform. Interpolation only works if the body is in Kinematic mode.</param>
        public virtual void SetBodyTransform(Vector3 p, Quaternion r, TeleportMode mode = TeleportMode.Interpolate)
        {
            SetBodyPosition(p, mode);
            SetBodyRotation(r, mode);
        }

        /// <summary>
        /// Unconditionally set the velocity.
        /// </summary>
        public virtual void SetBodyVelocity(Vector3 v)
        {
            string debugString = $"SetBodyVelocity({v})";
            if (IsFrozen)
            {
                savedVelocity = v;
                debugString += " => saved velocity";
            }
            else
            {
                if (!Body.isKinematic) // Should be redundant of IsFrozen, but just in-case.
                {
                    Body.linearVelocity = v;
                    debugString += " => active velocity";
                }
                else
                {

                }
            }
            Log(debugString);
        }

        public virtual void SetAngularVelocity(Vector3 w)
        {
            string debugString = $"SetAngularVelocity({w})";
            if (!Body.isKinematic)
            {
                Body.angularVelocity = w;
                debugString += " => active angular velocity";
            }
            Log(debugString);
        }

        /// <summary>
        /// Applies a physics impulse. Unlike the Combat ApplyVelocityX/Y, this does not cancel the existing X or Y motion.
        /// </summary>
        public virtual void ApplyImpulse(Vector3 impulse, bool ignoreMass)
        {
            if (!Body.isKinematic)
            {
                // Direct set velocity instead of using AddForce() so velocity changes are reflected immediately.
                // Context:
                //   AddForce() queues the impulse in a hidden buffer managed by Unity, and is popped during the internal FixedUpdate.
                //   This means that ApplyImpulse(a) -> SetVelocity(b) results in velocity=a+b, instead of velocity=b as you might expect.
                Body.linearVelocity += ignoreMass ? impulse : impulse * InverseMass;
            }
        }

        /// <summary>
        /// Applies a physics force. DeltaTime must be supplied since acceleration is not instantaneous.
        /// Returns the gravity impulse applied.
        /// </summary>
        public virtual Vector3 ApplyForce(Vector3 force, float deltaTime, bool ignoreMass)
        {
            Vector3 impulse = force * deltaTime;
            ApplyImpulse(impulse, ignoreMass);

            return impulse;
        }

        public virtual void ResetDynamicMotion()
        {
            Body.linearVelocity = Vector3.zero;
            Body.isKinematic = false;
        }

        protected virtual void Log(string s)
        {
            if (UseDebugLog)
            {
                Debug.Log($"{gameObject.name}: {s}");
            }
        }
    }
}