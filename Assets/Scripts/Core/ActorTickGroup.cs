namespace Core
{
    /// <summary>
    /// Describes the different phases of a single Unity frame.
    /// The phases are based on Unreal Engine's "Tick Groups" and are "physics-centric".
    /// In other words, the physics step is the most important operation in the game,
    /// and the game logic must step in tandum with physics.
    /// This grouping assumes the game is primarily using FixedUpdate() for its main logic
    /// and animators are using the "Animate Physics" Update mode.
    /// Refer to https://docs.unity3d.com/Manual/ExecutionOrder.html for how Unity organizes its event callbacks.
    /// </summary>
    public enum ActorTickGroup
    {
        Unknown,

        /// <summary>
        /// Represents the phase of logic before the physics step.
        /// Typically consists of processing game inputs to prepare for the physics step.
        /// In Unity, this is equivalent to the time frame when FixedUpdate() runs.
        /// </summary>
        PrePhysics,

        /// <summary>
        /// Represents when Animators update and evaluate their internal state machines.
        /// This means sampling animations from all active states. This occurs before the physics step.
        /// Unity does not provide user hooks to run during this phase.
        /// </summary>
        DuringAnimation,

        /// <summary>
        /// Represents the phase after Animators step but before Physics step.
        /// Animators will have fired their Animation Events and logic in this phase
        /// can depend and interpret those events to prepare for physics.
        /// Root Motion to Physics logic should go here.
        /// In Unity, this is equivalent to when the OnAnimatorMove() callback runs.
        /// </summary>
        PostAnimation,

        /// <summary>
        /// Represents the physics step. There are no user hooks to run during this phase.
        /// Note, you can run asynchronously to physics by using the Update() loop.
        /// </summary>
        DuringPhysics,

        /// <summary>
        /// Represents the final moments of the physics step, but before the results are pushed
        /// to Unity's Object transforms. In this phase, you can apply post-processing logic
        /// to reposition objects, and those positional changes should be(?) reflected in the final rendered frame.
        /// This phase is only available through Unity's OnAnimatorIK() callback,
        /// which has esoteric requirements to setup.
        /// </summary>
        BeforePhysicsWriteTranform,

        /// <summary>
        /// Represents the phase where user callbacks for OnTriggerX() and OnCollisionX() events occur.
        /// This occurs after the physics step (and write transform) but before post physics.
        /// </summary>
        DuringPhysicsEventDispatch,

        /// <summary>
        /// Represents the phase after the animation step but before the frame ends (is rendered?).
        /// Typically is used for logic that depends on the results of the physics step, 
        /// such as updating game state in response to collisions and trigger overlaps.
        /// 
        /// Can only be accessed in Unity by starting a Coroutine with yield new WaitForFixedUpdate();
        /// 
        /// Note that changes to rigidbody.position and won't be reflected until the next physics step.
        /// In order to get transform changes during this step to flush correctly, you will need to directly
        /// update GameObject.transform.position instead of rigidbody.position.
        /// This has a side consequence of causing physics recalcuations though.
        /// https://docs.unity3d.com/ScriptReference/Rigidbody-position.html
        /// </summary>
        PostPhysics,

        /// <summary>
        /// Represents the phase where the frame is rendered.
        /// Update() and LateUpdate() will be called during this phase (potentially zero or any number of times).
        /// </summary>
        Render,
    }
}