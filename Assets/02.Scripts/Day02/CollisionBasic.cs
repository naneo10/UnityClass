using UnityEngine;
/*
[Collider]
-물리적인 충돌이나 트리거 이벤트가 발생했을 때 호출
-실제 모델의 모양 그대로가 아니라 단순화된, 박스, 구, 캡슐 등으로 처리

[물리 충돌 이벤트]
-private void OnCollisionEnter(Collision collision){} //처음 충돌이 발생되었을 때
-private void OnCollisionStay(Collision collision){} //충돌이 유지되는 동안 매 프레임 마다 호출
-private void OnCollisionExit(Collision collision){} //충돌이 끝났을 때

[어떤 경우에 사용될까?]
-물리적 연산이 필요한 경우
-캐릭터가 벽에 부딪힐 때, 이동을 멈추가나 특정 반응을 해야할 때
-투사체가 적이나 벽과 충돌하면 파괴되는 겅우
-플레이어가 특정 오브젝트와 물리적 충돌이 일어나는 경우
-점프해서 바닥에 찾기하는 경우

[트리거 이벤트]
-private void OnTriggerEnter(Collider other){} //트리거 영역에 처음 진입했을 때
-private void OnTriggerStay(Collider other){} //트리거 영역에 머무르는 동안
-private void OnTriggerExit(Collider other){} //트리거 영역에서 벗어났을 때

[어떤 경우에 사용될까?]
-물리적 연산이 필요없는 경우(충돌없이 감지만 필요한 경우)
-텔레포트 기능
-아이템 감지
*/

