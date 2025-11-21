using Unity.VisualScripting;
using UnityEngine;

public class SkillInput : MonoBehaviour
{
    [SerializeField] private KeyCode key = KeyCode.Q;
    [SerializeField] private CoolDown cooldownTimer;
    [SerializeField] private TargetFinder targetFinder;
    [SerializeField] private string effectid = "Slash";
    [SerializeField] private Vector3 effectoffset = new Vector3(0.0f, 1.0f, 0.0f);

    private void Awake()
    {
        if (targetFinder == null)
        {
            targetFinder = GetComponent<TargetFinder>();
        }
    }

    void Update()
    {
        if (!Input.GetKeyDown(key)) return;

        if (cooldownTimer != null && !cooldownTimer.IsReady)
        {
            return;
        }
        Transform target = GetCastTarget();

        PlaySkillEffect(target);
        if (cooldownTimer != null)
        {
            cooldownTimer.Trigger();
        }
    }

    private Transform GetCastTarget()
    {
        if (targetFinder == null) return transform;

        Transform foundTarget = targetFinder.GetTarget();

        if (foundTarget != null)
        {
            return foundTarget;
        }
        return transform;
    }

    private void PlaySkillEffect(Transform target)
    {
        if (EffectManager.Instance == null) return;

        if (string.IsNullOrEmpty(effectid))
        {
            return;
        }
        Vector3 spawnPosition = target.position + effectoffset;

        EffectManager.Instance.Play(effectid, spawnPosition);
    }
}
