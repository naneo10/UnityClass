using UnityEngine;

//게임에서 Vehicle이라는 기본 클래스가 있고 car라는 Truck이라는 두개의 파생클래스
public class Vehicle
{
    public float speed = 100.0f;
    public Vector3 direction;

    public void GoForward() //전진
    {

    }

    public void Reverse() //후진
    {

    }

    public void TurnRight() //오른쪽으로 회전
    {

    }

    public void TurnLeft() //왼쪽으로 회전
    {

    }
}

public class Car : Vehicle
{

}

public class Truck : Vehicle
{

}

public class navigator
{
    public void Move(Vehicle vehicle)
    {
        vehicle.GoForward();
        vehicle.Reverse();
        vehicle.TurnRight();
        vehicle.TurnLeft();
    }
}

internal class UnRefactorLSP : MonoBehaviour
{

}
