using UnityEngine;
using MaskGame.Character;
using Random = UnityEngine.Random;

public abstract class NPCCharacter : MonoBehaviour
{
    protected NPCSettings npcSettings
    {
        get
        {
            if (GameManager.instance == null)
            {
                return FindAnyObjectByType<GameManager>()?.NPCSettings;
            }
            return GameManager.instance?.NPCSettings;
        }
    }

    protected Animator animator;

    protected MaskState maskClique;
    protected NPCZone owningZone;

    protected float spawnTime;
    protected Vector3 velocity;
    protected bool isSuspicious = false;

    // Methods

    public void Init(NPCZone owningZone)
    {
        animator = GetComponentInChildren<Animator>();

        this.owningZone = owningZone;
        spawnTime = Time.time;

        InitChar();
        InitTransform();
    }

    protected virtual void InitChar()
    {
        foreach (SkinnedMeshRenderer m in GetComponentsInChildren<SkinnedMeshRenderer>())
        {
            m.materials[0].color = npcSettings.ColorFromMask(maskClique);
        }
    }

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
