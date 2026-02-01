using System.Collections;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class HallwayGuy : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float moveDuration = 3.0f; 
    public float waitTime = 1.0f; 

    private void Start()
    {
        StartCoroutine(MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return StartCoroutine(MoveToTarget(pointA, pointB, moveDuration));
            yield return new WaitForSeconds(waitTime); 

            yield return StartCoroutine(MoveToTarget(pointB, pointA, moveDuration));
            yield return new WaitForSeconds(waitTime); 
        }
    }

    private IEnumerator MoveToTarget(Vector3 startPoint, Vector3 endPoint, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;

            t = Mathf.SmoothStep(0f, 1f, t);

            transform.position = Vector3.Slerp(startPoint, endPoint, t);

            elapsedTime += Time.deltaTime; 
            yield return null; 
        }

        transform.position = endPoint;
        var direction = (endPoint - startPoint).normalized;
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction.normalized);
        }
    }
}

