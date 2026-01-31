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

        OnStartPeriod(Period);
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemaining -= Time.deltaTime;
        if (TimeRemaining < 0.0f)
        {
            OnTimeLimitReached();
        }
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
