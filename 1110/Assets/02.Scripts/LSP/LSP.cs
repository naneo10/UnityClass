using UnityEngine;

//회전할 수 있는 객체라면 이 기능을 가져야 한다.
public interface ITrunable
{
    public void TurnRight();
    public void TurnLeft();
}

//움직일 수 있는 객체라면 이 기능을 가져야 한다.
public interface IMovable
{
    public void GoForward();
    public void Reverse();
}

//도로에서 움직이는 탈 것(자동차, 오토바이 등)
public class RoadVehicle : IMovable, ITrunable
{
    public float speed = 100.0f;
    public float turnSpeed = 5.0f;

    public virtual void GoForward() { }
    public virtual void Reverse() { }
    public virtual void TurnLeft() { }
    public virtual void TurnRight() { }
}

public class Car1 : RoadVehicle
{
    public override void GoForward() { }
    //자동차를 앞으로 이동시키는 로직
    public override void Reverse() { }
    public override void TurnLeft() { }
    public override void TurnRight() { }
}

public class RailVehicle : IMovable
{
    public float speed = 200.0f;
    public virtual void GoForward() { }
    public virtual void Reverse() { }
}

public class Train : RailVehicle
{
    public override void GoForward() { }
    public override void Reverse() { }
}

public class LSP : MonoBehaviour
{
    void Start()
    {
        IMovable move1 = new Car1();
        IMovable move2 = new Train();
    }

    void Update()
    {
        
    }
}
