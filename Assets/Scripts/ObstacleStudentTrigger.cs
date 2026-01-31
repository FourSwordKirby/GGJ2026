using UnityEngine;

public class ObstacleStudentTrigger : MonoBehaviour
{
    public ObstacleStudent TriggerOwner;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Obstacle TriggerEntered");

        TriggerOwner.OnStudentCollision(null);
    }
}
