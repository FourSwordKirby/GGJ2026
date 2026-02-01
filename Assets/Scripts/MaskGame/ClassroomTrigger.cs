using UnityEngine;

public class ClassroomTrigger : MonoBehaviour
{
    public Classroom Owner;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("TriggerEntered");
        GameManager.instance.OnClassroomReached.Invoke(Owner);
    }
}
