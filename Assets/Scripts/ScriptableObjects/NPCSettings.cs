using UnityEngine;
using MaskGame.Character;

[CreateAssetMenu(fileName = "NPCSettings", menuName = "ScriptableObjects/NPCSettings")]
public class NPCSettings : ScriptableObject
{
    [Header("Jock")]
    public Color jockColor;
    public float jockRunRadius;
    public float jockRunVelocity;

    [Header("Cheerleader")]
    public Color cheerColor;
    public float cheerHopDist;

    [Header("Business")]
    public Color businessColor;
    public float businessWalkSpeed;
    public Vector2 businessMarchTimeRange;

    [Header("Nerd")]
    public Color nerdColor;
    public float nerdWalkSpeed;
    public Vector2 nerdMarchTimeRange;
    public float nerdRotationMax;
    public float nerdRotationSpeed;

    [Header("Theater")]
    public Color theaterColor;
    public float theaterMoveSpeed;

    [Header("Misc")]
    public Color obstacleColor;

    public Color ColorFromMask(MaskState mask)
    {
        switch (mask)
        {
            case MaskState.JOCK: return jockColor;
            case MaskState.CHEER: return cheerColor;
            case MaskState.BUSINESS: return businessColor;
            case MaskState.NERD: return nerdColor;
            case MaskState.THEATER: return theaterColor;
            default: return Color.white;
        }
    }

    // Jock

    public float JockCentripetalAccel()
    {
        return (jockRunVelocity * jockRunVelocity) / jockRunRadius;
    }

    public void ApplyObstacleColor(GameObject gameObj)
    {
        foreach (SkinnedMeshRenderer m in gameObj.GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            m.materials[0].color = obstacleColor;
        }
    }
}
