using UnityEngine;

/*
[인터페이스]
팩토리에서 만들어지는 모든 제품의 공통규칙을 정의
*/
public interface IProduct
{
    public string ProductName { get; set; }
    public void Initialize();
}
