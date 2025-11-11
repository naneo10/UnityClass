/*
위반 예시
-너무 많은 기능을 포함하고 있어 이를 구현하는 클래스가 필요하지 않은 메서드까지 구현해야하는 문제가 있다.
1.서로 다른 책임이 한 곳에 묶여 있다.
2.불필요한 구현 강제
*/
public interface IUnRefactorISP
{
    public float Health { get; set; }
    public int Defense { get; set; }
    public void Die();
    public void TakeDamage();
    public void RestoreHealth();
    public float MoveSpeed { get; set; }
}
