using UnityEngine;

public enum CliqueKind
{
    JOCK,
    CHEERLEADER,
    BUSINESS,
    NERD,
    THEATER
}

public class NPCCharacter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private NPCSettings npcSettings;

    [Header("Debug Only")]
    [SerializeField] private CliqueKind cliqueKind;
    [SerializeField] private NPCZone npcZone;

    private Rigidbody rb;
    private float spawnTime;

    public void SetClique(CliqueKind cliqueKind)
    {
        this.cliqueKind = cliqueKind;
    }

    void Start()
    {
        // npcZone = GetComponentInParent<NPCZone>();
        rb = GetComponent<Rigidbody>();
        spawnTime = Time.time;
    }

    void Update()
    {
        Move(fFixedUpdate: false);
    }

    void FixedUpdate()
    {
        Move(fFixedUpdate: true);
    }

    void Move(bool fFixedUpdate)
    {
        switch (cliqueKind)
        {
            case CliqueKind.JOCK:
                {
                    if (!fFixedUpdate)
                        return;

                    // Move in circle

                    Vector3 velocityCur = rb.linearVelocity;
                    Vector3 normalCur = Vector3.Normalize(velocityCur);

                    float accel = npcSettings.JockCentripetalAccel();

                    Vector3 velocityNext = velocityCur + Time.fixedDeltaTime * normalCur * accel;
                    rb.linearVelocity = velocityNext;
                }
                break;

            case CliqueKind.CHEERLEADER:
                {
                    if (fFixedUpdate)
                        return;

                    // Hop, hop, left, hop, hop, right

                }
                break;


            case CliqueKind.BUSINESS:
                {
                    if (fFixedUpdate)
                        return;

                    // Move back til edge
                }
                break;

            case CliqueKind.NERD:
                {
                    if (fFixedUpdate)
                        return;

                    // Periodically 
                }
                break;

            case CliqueKind.THEATER:
                {
                    if (fFixedUpdate)
                        return;

                    // Change direction on timer

                }
                break;
        }
    }
}
