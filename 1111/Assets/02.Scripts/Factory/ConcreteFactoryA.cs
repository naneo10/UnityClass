using UnityEngine;

//Factory(추상클래스)를 상속받은 쿠체적인 공장 클래스
//실제로 ProductA(제품A)를 만들어서 반환하는 역할
public class ConcreteFactoryA : Factory
{
    [SerializeField] private ProductA m_ProductPrefab;

    public override IProduct GetProduct(Vector3 position)
    {
        //생성
        GameObject instance = Instantiate(
            m_ProductPrefab.gameObject, 
            position, 
            Quaternion.identity);

        //생성된 오브젝트에서 ProductA 컴포넌트 가져오고
        ProductA newProduct = instance.GetComponent<ProductA>();

        //제품 고유의 초기화 메서드 실행
        newProduct.Initialize();

        return newProduct;
    }
}
