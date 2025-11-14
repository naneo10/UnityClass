using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioObserver : MonoBehaviour
{
    [SerializeField] ButtonSubject subjectToObserver;
    [SerializeField] float delay = 0.0f;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if (subjectToObserver != null)
        {
            subjectToObserver.Clicked += OnthingHappened;
        }
    }

    public void OnthingHappened()
    {
        StartCoroutine(PlayWithDelay());
    }

    IEnumerator PlayWithDelay()
    {
        yield return new WaitForSeconds(delay);

        audioSource.Stop();
        audioSource.Play();
    }

    private void OnDestroy()
    {
        {
            if (subjectToObserver != null)
            {
                subjectToObserver.Clicked -= OnthingHappened;
            }
        }
    }
}
