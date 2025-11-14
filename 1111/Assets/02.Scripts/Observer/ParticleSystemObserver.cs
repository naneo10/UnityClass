using UnityEngine;

public class ParticleSystemObserver : MonoBehaviour
{
    [SerializeField] private ButtonSubject m_SubjectToObserver;
    [SerializeField] private ParticleEffect m_Effect;

    [SerializeField] private Transform m_SpawnPoint;

    private void Awake()
    {
        if (m_SubjectToObserver != null)
        {
            m_SubjectToObserver.Clicked += OnThingHappened;
        }
    }

    private void OnThingHappened()
    {
        if (!m_Effect) return;

        if (m_SpawnPoint)
        {
            m_Effect.PlayAt(m_SpawnPoint.position);
        }
        else
        {
            m_Effect.PlayNow();
        }
    }

    private void OnDestroy()
    {
        if (m_SubjectToObserver != null)
        {
            m_SubjectToObserver.Clicked -= OnThingHappened;
        }
    }
}
