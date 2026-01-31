using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

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
    public Classroom Objective;
    public float TimeRemaining;

    public Action OnStartPeriod;
    public Action OnTimeLimitReached;
    public Action OnObjectiveReached;

    private void Start()
    {
        OnTimeLimitReached += FailLevel;
        OnObjectiveReached += AdvancePeriod;

        StartPeriod();
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemaining -= Time.deltaTime;
    }

    void FailLevel()
    {
        Debug.Log("Level Failed");
    }

    void AdvancePeriod()
    {
        Period += 1;
        if (Period >= MaxPeriod)
            Debug.Log("Max Period Reached");
        else
            StartPeriod();
    }

    void StartPeriod()
    {
        TimeRemaining = MaximumTime;
        OnStartPeriod.Invoke();
    }
}

/// <summary>
/// This class defines a level on a primitive level (i.e. classrooms that can be valid objective locations, hallways, etc)
/// </summary>
public class LevelManager
{
    public List<Classroom> classrooms;
}
