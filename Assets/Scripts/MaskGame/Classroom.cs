using UnityEngine;

[ExecuteInEditMode]
public class Classroom : MonoBehaviour
{
    public string ClassroomName;

    [SerializeField]
    private Material NeutralMaterial;
    [SerializeField]
    private Material GoalMaterial;
    [SerializeField]
    private Transform snapTransform;

    public Renderer SelfRenderer;

    public TMPro.TextMeshPro ClassroomLabel;

    public void Update()
    {
        ClassroomLabel.text = ClassroomName;
    }

    public void SetAsGoal()
    {
        SelfRenderer.material = GoalMaterial;
    }

    public void SetAsNeutral()
    {
        SelfRenderer.material = NeutralMaterial;
    }

    public Transform GetSnapTransform()
    {
        return snapTransform;
    }
}
