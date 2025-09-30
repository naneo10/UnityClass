using UnityEngine;
/*
[Vector]
-수학 개념 : 크기(길이)와 방향을 가진 값
:방향(Diraction) : 어디로 가는지
:크기(Magnitude) : 얼마나 가는지
-게임 개발 : 위치, 이동, 속도, 힘 등을 표현할 때 사용

x, y
-(0, 0) 원점
-(0, 1) 위로 1만큼 이동
-(1, 0) 오른쪽으로 1만큼 이동

[Vector2]
-2차원 벡터
-주로 2D 게임에서 위치, 이동 속도 등을 표현할 떄 사용
-(3, 4) -> x축으로 3만큼, y축으로 4만큼 이동

[Vector3]
-3차원 벡터 (x,y,z)
-주로 3D 게임에서 위치, 이동, 속도 등을 표현할 때 사용
-(3, 4, 5) -> x축으로 3만큼, y축으로 4만큼 이동, z축으로 5만큼 이동

[벡터 덧셈]
-개념: 현재 위치 + 이동량 = 새로운 위치
-위치 이동을 계산할 때 사용
-예시: 플레이어가 (1, 2)에 있고 (3, 4)만큼 이동하면 새로운 위치는 (4, 6)

[벡터 뺄셈]
-두 점 사이의 방향이나 거리를 구할 때 사용
-목표위치 - 내 위치 = 목표까지의 방향벡터
-예시: 적이 플레이어를 쫒아올 때

[magnitude]
-거리, 속력 계산할 때 사용
-의미: 벡터가 얼마나 긴지
-예시: 벡터(3, 4)의 크기 : 5 /루트
*/
public class Vector : MonoBehaviour
{
    public Vector2 vectorA = new Vector2(2, 3);
    public Vector2 vectorB = new Vector2(4, 1);

    public Vector2 resultVector;
    public int input = 0;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            input = 1;
            Debug.Log(resultVector);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            input = 2;
            Debug.Log(resultVector);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            input = 0;
            Debug.Log(resultVector);
        }
    }

    private void OnDrawGizmos()
    {
        DrawVector(Vector2.zero, vectorA, Color.red);
        DrawVector(Vector2.zero, vectorB, Color.blue);

        if(input == 1)
        {
            resultVector = vectorA + vectorB;
            DrawVector(Vector2.zero, resultVector, Color.green);
        }
        else if (input == 2)
        {
            resultVector = vectorA - vectorB;
            DrawVector(Vector2.zero, resultVector, Color.cyan);
        }
    }

    void DrawVector(Vector2 start, Vector2 end, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawLine(start, end);
        Gizmos.DrawSphere(end, 0.1f);
    }
}
