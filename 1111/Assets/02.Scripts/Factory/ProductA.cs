using UnityEngine;

//팩토리에서 생성될 제품중 하나
//IProduct 인터페이스를 구현해서 공통된 규칙을 따름
public class ProductA : MonoBehaviour, IProduct
{
    [SerializeField] private string m_ProductName = "Product A";

    public string ProductName
    {
        get { return m_ProductName; }
        set { m_ProductName = value; }
    }

    private ParticleSystem m_particleSystem;

    public void Initialize()
    {
        gameObject.name = m_ProductName;

        m_particleSystem = GetComponentInChildren<ParticleSystem>();
        if (m_particleSystem == null) return;

        m_particleSystem.Stop();
        m_particleSystem.Play();
    }
}
