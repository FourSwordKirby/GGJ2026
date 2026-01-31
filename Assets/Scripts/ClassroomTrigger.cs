using UnityEngine;

public class ClassroomTrigger : MonoBehaviour
{
    public Classroom TriggerOwner;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEntered");
        GameManager.OnClassroomReached.Invoke(TriggerOwner);
    }
}
