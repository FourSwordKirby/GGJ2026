using MaskGame;
using MaskGame.Character;
using MaskGame.Cheerleader;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public enum GamePhase
    {
        GameStart,
        PeriodStart,
        PeriodInProgress,
        PeriodEnd,
        LevelComplete,
        GameOver
    }

    public GamePhase currentPhase;

    public LevelManager LevelManager;

    /// <summary>
    /// The amount of time the player has to reach the objective
    /// </summary>
    public const int MaxPeriod = 1;
    /// <summary>
    /// The amount of time the player has to reach the objective
    /// </summary>
    public float MaximumTime = 100.0f;

    /// <summary>
    /// This float represents the player's current popularity. If it dips below 0, it's game over;
    /// </summary>
    public float Popularity = 100.0f;

    /// <summary>
    /// This float represents the player's current popularity. If it dips below 0, it's game over;
    /// </summary>
    public const float MaximumPopularity = 100.0f;

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
    public float MismatchedTimeUpperBound = 5.0f;

    /// <summary>
    /// The amount of time the player has been in a mismatched mask state
    /// </summary>
    public float MismatchedMaskStateDuration = 0.0f;

    public float PopularityRecoveryPerSecond = 10;

    public PlayerCharacter player;
    public CheerleaderManager CheerleaderManager;

    public int Period;
    public Classroom GoalClassroom;
    public float TimeRemaining;

    public Action<int> OnStartPeriod;
    public Action OnTimeLimitReached;
    public Action<Classroom> OnClassroomReached;

    // singleton design pattern
    public static GameManager instance;

    private void OnValidate()
    {
        CheerleaderManager = GetComponentInChildren<CheerleaderManager>();
    }

    private void Start()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        InputSystem.actions.FindAction("Move").ReadValue<Vector2>();
        Keyboard.current.spaceKey.IsPressed();
        OnStartPeriod += StartPeriod;
        OnTimeLimitReached += OutOfTime;
        OnClassroomReached += PassPeriod;

        player = FindObjectsByType<PlayerCharacter>(FindObjectsSortMode.InstanceID)[0];
        AudioManager.instance.StartLevelMusic();

        OnStartPeriod(Period);
    }

    private void OnDestroy()
    {
        instance = null;
    }

    // Update is called once per frame
    // dumb hack
    float phaseTransitionTime = 0.0f;
    float fadeTime = 2.0f;
    void Update()
    {
        switch (currentPhase)
        {
            case GamePhase.PeriodStart:
                while (phaseTransitionTime < fadeTime)
                {
                    phaseTransitionTime += Time.deltaTime;
                    break;
                }
                currentPhase = GamePhase.PeriodInProgress;
                break;
            case GamePhase.PeriodInProgress:
                UpdateTimeRemaining();
                UpdatePopularity();
                break;
            case GamePhase.PeriodEnd:
                while (phaseTransitionTime < fadeTime)
                {
                    phaseTransitionTime += Time.deltaTime;
                    break;
                }
                AdvancePeriod();
                currentPhase = GamePhase.PeriodStart;
                phaseTransitionTime = 0.0f;
                break;
            case GamePhase.LevelComplete:
                break;
            case GamePhase.GameOver:
                if (Keyboard.current.rKey.wasPressedThisFrame)
                {
                    currentPhase = GamePhase.GameStart;
                }
                break;
        }
        if (Keyboard.current.rKey.IsPressed())
        {
            Reload();
        }
    }

    public void PassPeriod(Classroom classroom)
    {
        if(classroom == GoalClassroom)
        {
            AudioManager.instance.PlayPeriodPassed ();
            ScreenTransitionManager.instance.FadeOut(2.0f);
            PromptUI.ShowPrompt("Period Cleared!");
            currentPhase = GamePhase.PeriodEnd;
        }
    }

    public void RegisterLevelComplete()
    {
        currentPhase = GamePhase.LevelComplete;
    }

    public void OutOfTime()
    {
        ScreenTransitionManager.instance.FadeOut(fadeTime);
        AudioManager.instance.PlayOutOfTime();

        PromptUI.ShowPrompt($"Out of Time");
        currentPhase = GamePhase.GameOver;
    }
    public void OutOfPopularity()
    {
        ScreenTransitionManager.instance.FadeOut(fadeTime);

        PromptUI.ShowPrompt($"Out of Popularity");
        currentPhase = GamePhase.GameOver;
    }
    private string FormatPeriod(int value)
    {
        var digit = value % 10;
        return (value != 11 && digit == 1) ? value + "st" :
           value != 12 && digit == 2 ? value + "nd" :
           value != 13 && digit == 3 ? value + "rd" :
           value + "th";
    }

    void UpdateTimeRemaining()
    {
        TimeRemaining -= Time.deltaTime;
        if(TimeRemaining + Time.deltaTime >= 10.0f && 10.0f > TimeRemaining)
        {
            AudioManager.instance.PlayLast10Seconds();
        }

        if (TimeRemaining < 0.0f)
            OnTimeLimitReached();
    }

    void UpdatePopularity()
    {
        // While the player's mask is mismatched, tick the timer up.
        if (player.IsInMismatchedZone())
        {
            MismatchedMaskStateDuration += Time.deltaTime;
            PromptUI.instance.ShowAlert = true;
        }
        else
        {
            MismatchedMaskStateDuration = 0;
            Popularity += PopularityRecoveryPerSecond * Time.deltaTime;
        }

        float decayCurvePoint = MismatchedMaskStateDuration / MismatchedTimeUpperBound;
        float currentDecayRate = PopularityDecayCurve.Evaluate(decayCurvePoint) * MaximumPopularityDecayRate;
        Popularity -= currentDecayRate * Time.deltaTime;
        Popularity = Math.Clamp(Popularity, 0, MaximumPopularity);
        if (Popularity <= 0)
        {
            OutOfPopularity();
        }
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void AdvancePeriod()
    {
        Period += 1;
        if (Period >= MaxPeriod)
        {
            Debug.Log("Max Period Reached");
            TempUIManager.DisplayWin(Reload);
        }
        else
            StartPeriod(Period);
    }

    void StartPeriod(int period)
    {
        ScreenTransitionManager.instance.FadeIn(2.0f);
        string periodName = FormatPeriod(period);
        PromptUI.ShowPrompt($"Get to {periodName} Period!");
        currentPhase = GamePhase.PeriodStart;

        TimeRemaining = MaximumTime;
        LevelManager.StartPeriod(period);
    }
}

/// <summary>
/// This class defines a level on a primitive level (i.e. classrooms that can be valid objective locations, hallways, etc)
/// </summary>
[Serializable]
public class LevelManager
{
    public List<Period> Periods;

    public void StartPeriod(int period)
    {
        if(period > Periods.Count)
        {
            Debug.Log("YOU COMPLETED THEM ALL, Reloading the scene and restarting the game");
            GameManager.instance.Reload();
        }

        foreach(Period p in Periods)
        {
            foreach (var obj in p.RelevantObjects)
            {
                obj.SetActive(true);
            }
            p.GoalClassroom.SetAsNeutral();

        }

        foreach (var obj in Periods[period].RelevantObjects)
        {
            obj.SetActive(true);
        }
        Periods[period].GoalClassroom.SetAsGoal();
    }

    //internal Classroom SelectGoalClassroom(Classroom previousClassroom)
    //{
    //    var SelectableClassrooms = Classrooms.Where(x => x != previousClassroom).ToList();
    //    return SelectableClassrooms[Random.Range(0, SelectableClassrooms.Count)];
    //}
}

[Serializable]
public class Period
{
    public Classroom GoalClassroom;
    public List<GameObject> RelevantObjects;
}
