using UnityEngine;

/*
상속
부모의 기능을 자식이 물려받는 것
말 그대로 부모클래스가 가진 기능을 파생클래스가 그대로 물려받는 것

탈 것 = 부모클래스
:이동한다, 속도가 있다, 방향이 있다

자동차, 오토바이, 버스 = 자식클래스
:자동차 -> 기어변속 기능 추가 :오토바이 -> 균형잡기 기능 추가 :버스 -> 승객 태우는 기능을 추가

공통된 기능은 부모가 제공
각 탈 것만의 특징은 자식이 추가 또는 수정

^상속을 쓰는 이유는?
:공통 기능을 한 번만 만들어두고 여러 클래스가 사용가능
:유지보수 쉬움, 결합도가 높아지는 단점
:같은 종류라는 개념을 코드로 표현(플레이어가 총을 쏜다. 적도 총을 쏜다 -> 둘다 Shooter)
*/
public class ShootBase : MonoBehaviour
{
    [SerializeField] protected GameObject bulletPrefab;
    [SerializeField] protected float fireInterval = 0.5f;
    [SerializeField] protected Transform[] firePoints;

    protected float lastFireTime;

    protected bool canFire()
    {
        return Time.time - lastFireTime >= fireInterval;
    }

    //실제 발사 처리하는 메서드
    protected void Fire()
    {
        //아직 발사할 시간이 안됐으면 끝내라
        if (!canFire()) return;

        //프리팹이 없거나 파이어 포닝트가 없거나 길이가 0이면 그냥 끝내라
        if (bulletPrefab == null || firePoints == null || firePoints.Length == 0) return;

        //firePoints배열을 이용해 여러 총구에서 동시에 총알발사
        for (int i = 0; i < firePoints.Length; i++)
        {
            Transform firePoint = firePoints[i];
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        }

        //마지막 발사시간 갱신
        lastFireTime = Time.time;
    }
}
