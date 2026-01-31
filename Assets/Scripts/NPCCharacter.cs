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

    private float spawnTime;

    private Vector3 velocity;

    // Cheerleader

    private float cheerHopTime;

    // Nerd

    private float nerdMarchDuration;
    private float nerdTargetRotation;

    // Methods

    void Start()
    {
        // npcZone = GetComponentInParent<NPCZone>();
        spawnTime = Time.time;
    }

    public void Init(CliqueKind cliqueKind)
    {
        this.cliqueKind = cliqueKind;
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
                    velocity = transform.forward * npcSettings.jockRunVelocity;
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

                    velocity = -transform.forward * npcSettings.businessWalkSpeed;
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
                    // Correct to the side to center movement

                    transform.position += transform.right * npcSettings.theaterMoveSpeed * npcSettings.theaterSwitchTime / 2;

                    velocity = TheaterDirSign() * transform.right * npcSettings.theaterMoveSpeed;
                }
                break;
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        switch (cliqueKind)
        {
            case CliqueKind.JOCK:
                {
                    // Move in circle

                    Vector3 normalCur = Vector3.Normalize(Vector3.Cross(velocity, Vector3.up));

                    float accel = npcSettings.JockCentripetalAccel();

                    Vector3 velocityNext = velocity + Time.fixedDeltaTime * normalCur * accel;
                    velocityNext.y = 0;
                    velocity = velocityNext;

                    // Look towards velocity

                    transform.rotation = Quaternion.LookRotation(velocity);
                }
                break;

            case CliqueKind.CHEERLEADER:
                {
                    // Hop, hop, left, hop, hop, right

                    float dTFromTime = cheerHopTime - Time.time;
                }
                break;


            case CliqueKind.BUSINESS:
                {
                    // Move back til edge

                    if (npcZone)
                    {

                    }
                }
                break;

            case CliqueKind.NERD:
                {
                    // Periodically pause and rotate
                }
                break;

            case CliqueKind.THEATER:
                {
                    velocity = TheaterDirSign() * transform.right * npcSettings.theaterMoveSpeed;
                }
                break;
        }

        transform.position += velocity * Time.deltaTime;
    }

    // Theater

    float TheaterDirSign()
    {
        float dTSpawn = Time.time - spawnTime;
        return (Mathf.FloorToInt(dTSpawn / npcSettings.theaterSwitchTime) % 2 == 0) ? -1 : 1;
    }
}
