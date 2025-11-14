using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem ps;

    private void Awake()
    {
        if (!ps)
        {
            ps = GetComponent<ParticleSystem>();
        }
    }
    
    public void PlayNow()
    {
        if (!ps) return;

        ps.Stop();
        ps.Play();
    }

    public void PlayAt(Vector3 pos)
    {
        transform.position = pos;
        PlayNow();
    }
}
