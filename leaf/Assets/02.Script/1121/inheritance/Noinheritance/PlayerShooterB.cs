using UnityEngine;

public class PlayerShooterB : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; //프리팹
    [SerializeField] private float fireInterval = 0.2f; //발사간격
    [SerializeField] private Transform[] firePoints; //총알이 나갈 위치들

    private float lastFireTime; //마지막으로 발사한 시간

    void Update()
    {
        //마우스 왼쪽 버튼을 누르면 발사
        if (Input.GetMouseButton(0))
        {
            TryFire();
        }
    }

    //발사 가능한지 체크하는 메서드
    //발사해도 되는 시간이냐? 라는걸 체크하는 메서드
    //fireInterval시간이 지나야 다시 발사 가능
    private bool canFire()
    {
        return Time.time - lastFireTime >= fireInterval;
    }

    //실제 발사 처리하는 메서드
    private void TryFire()
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
