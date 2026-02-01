using UnityEngine;

public class CompassUI : MonoBehaviour
{

    void Update()
    {
        // figure out how to orient this towards the objective
        Vector3 playerPosition = GameManager.instance.player.transform.position;
        Vector3 objectivePosition = GameManager.instance.GoalClassroom.transform.position;

        Vector3 dir = objectivePosition - playerPosition;
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
     
        transform.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
