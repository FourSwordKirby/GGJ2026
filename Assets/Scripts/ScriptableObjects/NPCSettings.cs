using UnityEngine;

[CreateAssetMenu(fileName = "NPCSettings", menuName = "ScriptableObjects/NPCSettings")]
public class NPCSettings : ScriptableObject
{
    [Header("Jock")]
    public float jockRunRadius;
    public float jockRunVelocity;

    [Header("Cheerleader")]
    public float cheerHopDist;
    public float cheerHopTime;

    [Header("Business")]
    public float businessWalkSpeed;

    [Header("Nerd")]
    public float nerdWalkSpeed;
    public Vector2 nerdMarchTimeRange;
    public Vector2 nerdRotationRange;
    public float nerdRotationSpeed;

    [Header("Theater")]
    public float theaterMoveSpeed;
    public float theaterSwitchTime;

    // Jock

    public float JockCentripetalAccel()
    {
        return (jockRunVelocity * jockRunVelocity) / jockRunRadius;
    }
}
