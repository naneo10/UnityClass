using UnityEngine;

//Factory(추상클래스)를 상속받은 쿠체적인 공장 클래스
//실제로 ProductA(제품A)를 만들어서 반환하는 역할
public class ConcreteFactoryB : Factory
{
    [SerializeField] private ProductB m_ProductPrefab;

    public override IProduct GetProduct(Vector3 position)
    {
        GameObject instance = Instantiate(
            m_ProductPrefab.gameObject, 
            position, 
            Quaternion.identity);

        ProductB newProduct = instance.GetComponent<ProductB>();
        newProduct.Initialize();

        instance.name = newProduct.ProductName;
        Debug.Log(GetLog(newProduct)); //A에는 없는 추가동작

        return newProduct;
    }
}
