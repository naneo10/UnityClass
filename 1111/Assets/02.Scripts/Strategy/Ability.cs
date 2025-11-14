using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [SerializeField] protected string m_AbilityName;
    [SerializeField] protected Sprite m_ButtonIcon;
    [SerializeField] protected ParticleSystem m_particleSystem;
    [SerializeField] protected AudioClip m_AudioClip;
    [SerializeField] protected Vector3 m_ParticleSystemOffset = new Vector3(0.0f, 1.0f, 0.0f);

    public Sprite ButtonIcon => m_ButtonIcon;

    public virtual void Use(GameObject obj)
    {
        PlaySound();
        PlayParticleFX(obj);
    }

    private void PlayParticleFX(GameObject user)
    {
        if (m_particleSystem != null)
        {
            Vector3 spawnPos = user.transform.position + user.transform.TransformDirection(m_ParticleSystemOffset);

            ParticleSystem ps = Instantiate(m_particleSystem, spawnPos, user.transform.rotation);

            ps.Stop();
            ps.Play();
        }
    }

    private void PlaySound()
    {
        if (m_AudioClip)
        {
            //오디오 매니저로 처리
            AudioManager.Instance.PlaySoundEffect(m_AudioClip);
        }
    }
}
