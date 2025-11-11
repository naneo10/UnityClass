using UnityEngine;
using UnityEngine.UIElements;

public abstract class Shape
{
    public abstract float GetArea();
}

public class Rectangle : Shape
{
    public float width;
    public float height;

    public Rectangle(float width, float height)
    {
        this.width = width;
        this.height = height;
    }

    public override float GetArea()
    {
        return width * height;
    }
}

public class Circle : Shape
{
    public float radius;

    public Circle(float radius)
    {
        this.radius = radius;
    }

    public override float GetArea()
    {
        return radius * radius * Mathf.PI;
    }
}

//면적계산 클래스
//어떤 Shape든 전달 받아 GetArea()를 호출
//다형성을 통해 새로운 도형이 추가되도 수정할 필요없음.
public class AreaCalculator
{
    public float GetArea(Shape shape)
    {
        return shape.GetArea();
    }
}

public class OCP : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
