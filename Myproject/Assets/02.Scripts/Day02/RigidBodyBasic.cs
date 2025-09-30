using UnityEngine;

public class RigidBodyBasic
{
    /*
    물리연산을 적용받을 수 있는 컴포넌트
    중력, 충돌, 마찰력 등의 물리 효과를 적용
    물리연산 적용: 중력 및 힘(Force, Torque)적용 가능
    충돌감지: 다른 컬라이더와 상호작용
    운동학적 움직임이 가능: 트랜스폼을 직접 조작하지 않고 움직임을 구현 가능

    [RigidBody3D 컴포넌트 속성]
    1.Mass(질량) 오브젝트의 질량. 충돌반응에 영향을 줌
    2.Drag(공기저항)
    3.Angular Drag(회전 저항력)
    4.Use Gravity(중력사용) 중력의 영향을 받을지 여후
    5.IsKinematic: 물리엔진의 영향을 받지 않고. MovePosition, MoveRotation 등으로 직접 제어 가능
    6.Interpolate(보간): 프레임률이 낮을 때 움직임을 부드럽게 함
    -None: 보간 사용 안함 (디폴트)
    -Interpolate: 이전 프레임 위치와 현재 위치 사이를 보간
    -Extrapolate: 현재 위치와 다음 위치 사이를 보간
    7.CollsitionDectection : 충돌감지 방식을 설정
    -Discreate: 기본 값. 일반적인 충돌감지에 활용
    -Continuous: 빠르게 움직이는 오브젝트의 충돌감지에 활용, 충돌 누락방지 효과적. 성능에 약간의 부담이 될 수 있음
    -Continuous Dynamic: 매우 빠르게 움직이는 오브젝트에 활용. 충돌 누락방지에 효과적. 성능에 부담이 될 수 있음(Continuous 보다)
    8.Constraints: 특정 축에 대한 이동 및 회전을 고정하는 옵션
    -Freeze Position: 해당 축에 대한 이동을 고정
    -Freeze Rotation: 해당 축에 대한 회전을 고정
    */
}
