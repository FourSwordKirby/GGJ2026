using MaskGame.Character;
using UnityEngine;

public class ClassroomTrigger : MonoBehaviour
{
    public Classroom Owner;

    void OnTriggerEnter(Collider other)
    {
        if (!other.transform.CompareTag("Player"))
            return;

        Debug.Log("TriggerEntered");
        GameManager.instance.OnClassroomReached.Invoke(Owner);
    }
}
