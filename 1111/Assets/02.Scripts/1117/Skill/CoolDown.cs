using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image cooldownMask;

    [Header("setting")]
    [SerializeField] private float cooldownDuration = 5.0f;

    private float remainingTime;
    private bool isRunning;

    public bool IsReady
    {
        get { return !isRunning; }
    }

    private void Awake()
    {
        if (cooldownMask == null)
        {
            cooldownMask = GetComponent<Image>();
        }
    }
    void Update()
    {
        if (!isRunning) return;

        remainingTime -= Time.deltaTime;

        if (cooldownMask != null)
        {
            float t = Mathf.Clamp01(remainingTime / Mathf.Max(0.0001f, cooldownDuration));

            cooldownMask.fillAmount = t;
        }

        if (remainingTime <= 0.0f)
        {
            isRunning = false;
            remainingTime = 0.0f;
            if (cooldownMask != null)
            {
                cooldownMask.fillAmount = 0.0f;
            }
        }
    }

    public void Trigger()
    {
        if (isRunning) return;

        isRunning = true;
        remainingTime = cooldownDuration;

        if (cooldownMask != null)
        {
            cooldownMask.fillAmount = 1.0f;
        }
    }
}
