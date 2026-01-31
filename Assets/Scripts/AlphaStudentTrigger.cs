using UnityEngine;

public class AlphaStudentTrigger : MonoBehaviour
{
    public AlphaStudent Owner;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Obstacle TriggerEntered");

        Owner.OnStudentCollision(null);
    }
}
