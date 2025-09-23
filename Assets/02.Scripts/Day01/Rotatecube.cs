using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
오일러 각(Euler Angle): 3차원 공간에서 회전을 표현하는 방법
차원 회전을 표현하는 방식. x,y,z 축에 대한 개별적인 회전 값을 사용하여 물체의 회전을 정의
직관적이지만, 짐벌락 문제가 발생될 수 있음

쿼터니언(Quaternion): 회전을 표현하는 또 다른 방법. 짐벌락 현상을 피할 수 있다
4차원 복소수로 표현 됨. (x, y, z, w) 
직관적이지 않음
*/

public class Rotatecube : MonoBehaviour
{
    /*
    [transform.Rotate]
    :Rotate(x, y, z, space.self)[default 값]또는 Rotate(x, y, z, space.world)로 축을 지정할 수 있음
    -현재 회전 값에서 입력된 Vector3만큼 더 회전(누적)

    [fransform.rotation]
    :Quaternion.Euler(x, y, z)를 사용해 특정각도로 직접 설정
    -기존 회전을 유지하지 않고, 덮어 씌우는 방식

    [transform.localRotation]
    :rotation과 유사하지만 부모 오브젝트의 영향을 받는 로컬 회전 값을 설정
    -부모 오브젝트가 회전하면 상대적인 회전 값이 변경될 수 있음

    [transform.LookAt]
    :특정 타겟을 바라보게 만듬
    -3D 게임에서 적이 플레이어를 향하도록 만들 때 사용

    [transform.RotateAround]
    :특정 위치를 중심으로 오브젝트를 회전
    -카메라가 특정 오브젝트를 도는 연출이나 위성 궤도 회전 등에 활용이 가능

    [Quaternion.LookRotation
    :LookAt과 유사하지만 더 유연한 방식
    -특정 방향을 향하게 할 때 사용
    */

    public Transform target;

    void Update()
    {
        transform.RotateAround(target.position, Vector3.down, 30.0f * Time.deltaTime);

        transform.Rotate(transform.up * -30.0f * Time.deltaTime); //오브젝트의 로컬 y축 //음수 양수로 방향 결정
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 50.0f, 0.0f)); //오브젝트 방향 상관없이 y축으로 회전
    }
}
