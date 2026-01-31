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

public abstract class NPCCharacter : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected NPCSettings npcSettings;

    [Header("Debug Only")]
    [SerializeField] protected CliqueKind cliqueKind;
    [SerializeField] protected NPCZone owningZone;

    protected float spawnTime;
    protected Vector3 velocity;
    protected bool isSuspicious = false;

    // Methods

    void Start()
    {
        owningZone = GetComponentInParent<NPCZone>();
        spawnTime = Time.time;
    }

    public void Init(NPCZone owningZone)
    {
        this.owningZone = owningZone;
        InitChar();
        InitTransform();
    }

    protected abstract void InitChar();
    protected abstract void InitTransform();
    protected abstract void UpdateMovement();

    void Update()
    {
        UpdateMovement();

        // Apply physics variables

        transform.position += velocity * Time.deltaTime;
    }

    // Suspicious

    void SetSuspicious(bool isSuspiciousNext)
    {
        if (isSuspicious == isSuspiciousNext)
            return;

        isSuspicious = isSuspiciousNext;

        if (isSuspicious)
        {
            // TODO (imonh) Enable headlook
            // TODO (imonh) Enable question
        }
        else
        {
            // TODO (imonh) Disable headlook
            // TODO (imonh) Disable question
        }
    }
}
