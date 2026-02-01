using UnityEngine;

[CreateAssetMenu(fileName = "NPCSettings", menuName = "ScriptableObjects/NPCSettings")]
public class NPCSettings : ScriptableObject
{
    [Header("Jock")]
    public float jockRunRadius;
    public float jockRunVelocity;

    [Header("Cheerleader")]
    public float cheerHopDist;

    [Header("Business")]
    public float businessWalkSpeed;
    public Vector2 businessMarchTimeRange;

    [Header("Nerd")]
    public float nerdWalkSpeed;
    public Vector2 nerdMarchTimeRange;
    public float nerdRotationMax;
    public float nerdRotationSpeed;

    [Header("Theater")]
    public float theaterMoveSpeed;

    // Jock

    public float JockCentripetalAccel()
    {
        return (jockRunVelocity * jockRunVelocity) / jockRunRadius;
    }
}
