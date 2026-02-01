using TMPro;
using UnityEngine;

public class PopularityUI : MonoBehaviour
{
    public Transform PopularityMeter;

    // Update is called once per frame
    void Update()
    {
        PopularityMeter.transform.localScale = new Vector3(1, GameManager.instance.Popularity/ GameManager.MaximumPopularity, 1);
    }
}
