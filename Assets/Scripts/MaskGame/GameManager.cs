using MaskGame.Character;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public LevelManager Level;

    /// <summary>
    /// The amount of time the player has to reach the objective
    /// </summary>
    public const int MaxPeriod = 7;
    /// <summary>
    /// The amount of time the player has to reach the objective
    /// </summary>
    public const float MaximumTime = 90.0f;

    /// <summary>
    /// This float represents the player's current popularity. If it dips below 0, it's game over;
    /// </summary>
    public float Popularity = 100.0f;

    /// <summary>
    /// This curve represents the rate that Popularity will decay over time when the player has the wrong mask for a given zone
    /// </summary>
    public AnimationCurve PopularityDecayCurve;

    /// <summary>
    ///  This represents the higest that the popularity decay rate will reach.
    /// </summary>
    public float MaximumPopularityDecayRate;

    /// <summary>
    /// As the player has a mismatched mask in a zone, popularity will decay over time. The decay rate advances from 0 to Maximum decay rate over the course of several seconds, defined by this variable
    /// </summary>
    public const float MismatchedTimeUpperBound = 3.0f;

    /// <summary>
    /// The amount of time the player has been in a mismatched mask state
    /// </summary>
    public float MismatchedMaskStateDuration = 0.0f;

    public PlayerCharacter player;

    public int Period;
    public Classroom ClassroomObjective;
    public float TimeRemaining;

    public static Action<int> OnStartPeriod;
    public static Action OnTimeLimitReached;
    public static Action<Classroom> OnClassroomReached;

    private void Start()
    {
        InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
        Keyboard.current.spaceKey.IsPressed();
        OnStartPeriod += StartPeriod;
        OnTimeLimitReached += FailLevel;
        OnClassroomReached += CheckObjectiveReached;

        player = FindObjectsByType<PlayerCharacter>(FindObjectsSortMode.InstanceID)[0];

        OnStartPeriod(Period);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimeRemaining();
        UpdatePopularity();
    }

    void UpdateTimeRemaining()
    {
        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining < 0.0f)
            OnTimeLimitReached();
    }

    void UpdatePopularity()
    {
        // While the player's mask is mismatched, tick the timer up.
        if (player.RequestedMaskState != MaskState.NONE && player.CurrentMaskState != player.RequestedMaskState)
            MismatchedMaskStateDuration += Time.deltaTime;
        else
            MismatchedMaskStateDuration = 0;

        float decayCurvePoint = MismatchedMaskStateDuration / MismatchedTimeUpperBound;
        float currentDecayRate = PopularityDecayCurve.Evaluate(decayCurvePoint) * MaximumPopularityDecayRate;
        Popularity -= currentDecayRate * Time.deltaTime;
    }

    void FailLevel()
    {
        Debug.Log("Level Failed");
    }

    void CheckObjectiveReached(Classroom classroom)
    {
        if(classroom == ClassroomObjective)
            AdvancePeriod();
    }

    void AdvancePeriod()
    {
        Period += 1;
        if (Period >= MaxPeriod)
            Debug.Log("Max Period Reached");
        else
            StartPeriod(Period);
    }

    void StartPeriod(int period)
    {
        Debug.Log($"Starting Period {period}");
        TimeRemaining = MaximumTime;
        ClassroomObjective = Level.classrooms[Random.Range(0, Level.classrooms.Count)];
    }
}

/// <summary>
/// This class defines a level on a primitive level (i.e. classrooms that can be valid objective locations, hallways, etc)
/// </summary>
[Serializable]
public class LevelManager
{
    public List<Classroom> classrooms;
}
