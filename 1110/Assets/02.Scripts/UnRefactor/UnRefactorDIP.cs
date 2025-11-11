using UnityEngine;
/*
-스위치 클래스는 토글 메서드를 호출해 문을 열고 닫을 수 있다.
-작동하기는 하지만 이 경우 도어에서 직접 스위치로 연결되는 종속성이 발생한다는 문제가 있다.
-스위치의 로직이 도어 외의 다른항목(조명을 켠다거나)에도 사용된다면?
-스위치 클래스에 메서드를 추가할 수 있겠지만 그러면 OCP도 위반하게 된다.
-기능을 확장하려 할 때마다 원본 코드를 수정해야 한다.
*/
public class Switch 
{
    private Door door;
    public bool isActive;
    public void Toggle()
    {
        if (isActive)
        {
            isActive = false;
            door.Open();
        }
        else
        {
            isActive = true;
            door.Close();
        }
    }
}
public class Door : MonoBehaviour
{
    public void Open() { }
    public void Close() { }
}