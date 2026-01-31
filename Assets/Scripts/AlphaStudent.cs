using UnityEngine;

/// <summary>
/// The types of students that roam the calls
/// </summary>
public enum StudentType
{
    Athelete,
    Bookworm,
    StudentCouncil
}


/// <summary>
/// This student is one which the player character needs to avoid running into. If the player runs into this student, they'll 
/// lose "reputation"
/// </summary>
public class AlphaStudent : MonoBehaviour
{
    public StudentType StudentType;

    public void OnStudentCollision(string PlayerCharacter)
    {
    }
}
