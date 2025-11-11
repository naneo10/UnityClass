using UnityEngine;

public class ProductB : MonoBehaviour, IProduct
{
    [SerializeField] private string m_ProductName = "ProductB";

    public string ProductName
    {
        get { return m_ProductName; }
        set { m_ProductName = value; }
    }

    private AudioSource audioSource;

    public void Initialize()
    {
        gameObject.name = m_ProductName;
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null) return;

        audioSource.Stop();
        audioSource.Play();
    }
}
