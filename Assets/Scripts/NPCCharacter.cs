using UnityEngine;
using Random = UnityEngine.Random;

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

    // Cheerleader

    private float cheerHopTime;

    // Nerd

    private float nerdMarchDuration;
    private float nerdTargetRotation;

    // Methods

    public void SetClique(CliqueKind cliqueKind)
    {
        this.cliqueKind = cliqueKind;
    }

    void Start()
    {
        // npcZone = GetComponentInParent<NPCZone>();
        rb = GetComponent<Rigidbody>();
        spawnTime = Time.time;

        InitTransform();
    }

    void InitTransform()
    {
        switch (cliqueKind)
        {
            case CliqueKind.JOCK:
                {
                    // Randomly rotate pivot by rotating the NPC

                    transform.Rotate(Vector3.up, Random.value * 360.0f);
                    rb.linearVelocity = transform.forward * npcSettings.jockRunVelocity;
                }
                break;

            case CliqueKind.CHEERLEADER:
                {
                    // Start in right hop position

                    transform.position += Vector3.right * (npcSettings.cheerHopDist / 2.0f);

                    cheerHopTime = Time.time;
                }
                break;

            case CliqueKind.BUSINESS:
                {
                    // Randomize into one of four cardinals

                    int signX = (Random.value < 0.5f) ? 1 : -1;
                    int signZ = (Random.value < 0.5f) ? 1 : -1;
                    Vector3 normalFace = new Vector3(signX, 0, signZ);
                    transform.rotation = Quaternion.LookRotation(normalFace);

                    rb.linearVelocity = -transform.forward * npcSettings.businessWalkSpeed;
                }
                break;

            case CliqueKind.NERD:
                {
                    // Randomize orientation, init tracking variables

                    transform.Rotate(Vector3.up, Random.value * 360.0f);


                }
                break;

            case CliqueKind.THEATER:
                {
                    rb.linearVelocity = TheaterDirSign() * transform.right * npcSettings.theaterMoveSpeed;
                }
                break;
        }
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
                    velocityNext.y = 0;
                    rb.linearVelocity = velocityNext;

                    // Look towards velocity

                    transform.rotation = Quaternion.LookRotation(velocityCur);
                }
                break;

            case CliqueKind.CHEERLEADER:
                {
                    if (fFixedUpdate)
                        return;

                    // Hop, hop, left, hop, hop, right

                    float dTFromTime = cheerHopTime - Time.time;
                }
                break;


            case CliqueKind.BUSINESS:
                {
                    if (fFixedUpdate)
                        return;

                    // Move back til edge

                    if (npcZone)
                    {

                    }
                }
                break;

            case CliqueKind.NERD:
                {
                    if (fFixedUpdate)
                        return;

                    // Periodically pause and rotate
                }
                break;

            case CliqueKind.THEATER:
                {
                    if (fFixedUpdate)
                        return;

                    rb.linearVelocity = TheaterDirSign() * transform.right * npcSettings.theaterMoveSpeed;
                }
                break;
        }
    }

    // Theater

    float TheaterDirSign()
    {
        float dTSpawn = Time.time - spawnTime;
        return (Mathf.FloorToInt(dTSpawn / npcSettings.theaterSwitchTime) % 2 == 0) ? -1 : 1;
    }
}
