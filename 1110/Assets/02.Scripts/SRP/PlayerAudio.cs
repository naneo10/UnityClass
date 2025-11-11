using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private float m_coolDownTime = 2.0f;
    [SerializeField] private AudioClip[] m_bounceClips;

    private float m_lastTimePlayed;
    private AudioSource m_audioSource;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        m_lastTimePlayed = -m_coolDownTime;
    }

    public void PlayRandomClip()
    {
        //다음 재생 가능한 시점 계산
        float timeToNextPlay = m_coolDownTime + m_lastTimePlayed;

        //현재 시간이 다음 재생 가능 시간보다 크면 (쿨다운이 끝났으면)
        if (Time.time > timeToNextPlay)
        {
            //갱신
            m_lastTimePlayed = Time.time;

            //가져와서 재생
            m_audioSource.clip = GetRandomClip();
            m_audioSource.Play();
        }
    }

    private AudioClip GetRandomClip()
    {
        int randomIndex = Random.Range(0, m_bounceClips.Length);
        return m_bounceClips[randomIndex];
    }
}
