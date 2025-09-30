using UnityEngine;
/*
[Prefabs]
-언제든지 재사용할 수 있는 미리 만들어진 게임오브젝트 에셋
-프리팹으로 만드려면 해당 게임 오브젝트를 하이어라키 창에서 프로젝트 창으로 드래그 & 드랍
-프리팹을 수정해서 오브젝트의 정보를 바꾸면 기존의 프리팹 인스턴스에서도 그 변화가 반영이 된다
*/
public class Bullet : MonoBehaviour
{
    private float moveSpeed = 8.0f;
    private Rigidbody bulletRigid;

    void Awake()
    {
        bulletRigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 3.0f);
    }

    void OnTriggerEnter(Collider other)
    {
        //태그가 플레이어
        if(other.CompareTag("Player"))
        {
            //PlayerController를 안전하게 가져오고
            if(other.TryGetComponent(out PlayerController playerController))
            {
                //플레이어의 Die 메서드 가져오자. (호출)
                playerController.Die();
                //총알이 적중한 후 즉시 파괴
                Destroy(gameObject);
            }
        }
    }

    //총알이 발사될 때 지정된 방향과 속도를 rigidbody에 적용해서 실제로 날아가게 함
    public void Shot (Vector3 dir, float speed)
    {
        //전달 받은 속도를 무브스피드로 저장
        moveSpeed = speed;
        //리지드의 속도를 지정된 방향 * 속도로 설정
        bulletRigid.velocity = dir * moveSpeed;
    }
}
