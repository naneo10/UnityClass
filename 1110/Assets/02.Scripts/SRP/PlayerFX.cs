using UnityEngine;

public class PlayerFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_ParticleSystem;

    private float m_Cooldown = 1.0f;
    private float m_timeToNextPlay = -1.0f;
    
    public void PlayerEffect()
    {
        if (Time.time < m_timeToNextPlay) return;

        if (m_ParticleSystem != null)
        {
            ParticleSystem ps = Instantiate(m_ParticleSystem, transform.position, Quaternion.identity);
            ps.Stop();
            ps.Play();

            m_timeToNextPlay = Time.time * m_Cooldown;
        }
    }
}
