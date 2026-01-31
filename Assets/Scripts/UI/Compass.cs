using UnityEngine;

public class Compass : MonoBehaviour
{
    public GameObject target;
    public Camera camera;

    void Update()
    {
        Vector3 screenPos = camera.WorldToScreenPoint(target.transform.position);

        Vector3 dir = screenPos - new Vector3(Screen.width / 2f, Screen.height / 2f);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
     
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
