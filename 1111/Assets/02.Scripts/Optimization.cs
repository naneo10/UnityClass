using UnityEngine;

public class Optimization : MonoBehaviour
{
    /*
    1.코드를 반드시 모든 프레임에 실행해야 하는지 확인
    -불필요한 로직을 update, fixedUpdate, LateUpdate에서 제외해야 한다
    -Awake, OnEnable, Start같은 메서드에서는 비용이 많이 드는 로직을 가급적 사용하지 말아야 함
    -쓰지 않은 업데이트 계열 메서드는 반드시 제거

    2.GetComponent, Find계열 메서드 사용을 줄이기 : 컴포넌트는 캐싱을 해서 가져오는게 좋다, 컴포넌트는 힙 영역에 올린다
    -자주 호출될 경우 성능에 악영향을 줌
    -따라서 객체 참조가 필요할 때 마다 업데이트에서 Get, Find계열 메서드들을 호출하는 방식은 지양하고
    -최대한 Awake나 Start에서 캐싱해서 사용

    private Rigidbody Rigidbody;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    3.GetComponent 대신 TryGetComponent를 사용

    4.Ojbect.name, GameObject.tag 사용 지양
    -게임 오브젝트의 이름을 참조해야할 때 .name프로퍼티를 호출
    -태그 비교를 할 때 .tag프로퍼티를 호출하고 ==, .Equals()등으로 비교
    -이런 호출 하나 하나가 가비지를 한 개씩 생성

    ^[Bad]
    private void Update()
    {
        if (gameObject.name =="Boss")
        {
            Debug.Log("");
        }
        if (agmeObject.tag =="Enemy")
        {
            Debug.Log("");
        }
    }

    ^[Good] : 차라리 상수로 처리
    private const string EnemyName = "Boss"
    
    private void Update()
    {
        if (gameObject.name == EnemeyName)
        {
            Debug.Log("");
        }
    }

    5.코루틴
    -코루틴도 캐싱하자
    -코루틴에서는 WaitForSeconds()등의 객체를 yield return으로 사용
    -그런데 'new' yield return new WaitForSeconds(0.1f)로 생성할 경우 모조리 가비지 수집 대상
    ^
    IEnumerator AttackCo()
    {
        WaitForSeconds wait = new WaitForSeconds(0.1f);    

        while (true)
        {
            yield return new WaitForSeconds(0.1f); //new로 계속 생성하지 말고 캐싱을 해야한다
            ㄴ yield return wait;
        }
    }

    ^
    필드에서 캐싱해서 쓰는 것도 좋다
    private WaitForSeconds wait;

    private void Awake()
    {
        wait = new WaitForSeconds(0.2f);
    }

    private void Start()
    {
        StartCoroutine(FireCo());
    }

    IEnumerator FireCo()
    {
        while (true)
        {
            Shoot();
            yield return wait;
        }
    }

    private void Shoot() {}

    6.문자열 파라미터 대신에 해시 값 사용
    -Animator.StringToHash

    7.트랜스폼 변경은 한 번
    -position, rotation, scale을 한 메서드 안에서 여러번 변경할 경우 그때마다 트랜스폼의 변경이 이루어짐
    -그런데, 트랜스폼이 여러 자식 트랜스폼을 갖고 있는 경우, 자식 트랜스폼도 함께 변경된다.
    -트랜스폼 변경 자체는 큰 연산이 아닐 수도 있지만 한 번에 이루어질 수 있는 연산을 여러번 하는 것은
        당연히 성능저하로 이어질 수 있음
    -따라서 벡터로 미리 담아두고 최종계산 이후 트랜스폼에 한 번만 변경을 지정하는 것이 좋다.
    -position, rotation 모두 변경해야 하는 경우 SetPositionAndRotation()메서드를 사용하는 것이 좋다

    ^Bad
    private Transform target;
    private float moveSpeed;

    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        dir.Normalize();
        transform.position += dir * moveSpeed * Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(dir);
    }

    ^Good
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        dir.Normalize();

        Vector3 newPos = transform.position + dir * moveSpeed * Time.deltaTime;
        Quaternion newRot = Qaternion.LookRotation(dir);
        
        transform.SetPositionAndRotation(newPos, newRot);
    }

    #stringBuilder:대규모 텍스트 변경
    
    8.new를 최소화하고 객체 재사용
    9.불필요한 부모자식 구조를 늘리지 않기
    10.스크립터블 오브젝트 활용하기
    11.오브젝트 풀링 활용하기

    단순한 데이터, 자주 사용하는 데이터 구조체 활용
    Vector3
    Vector2
    Quaternion

    데이터가 크고 공유되는 것들은
    Class

    12.나눗셈 대신 곰셈
    float a = 1f / 2f;
    float b = 1f * 0.5;

    13.컬렉션 재사용하기
    ^Bad
    private void Update()
    {
        DetectEnemies();
    }

    void DetectEnemies()
    {
        List<Transform> enemyList = 'new' List<Transform>(); //new를 주의

        foreach (var enemy in enemyList)
        {
            if (Vector3.Distance(transform.position, enemy.position) < 10.0f)
            {
                enemyList.Add(enemy);
            }
        }
    }

    ^Good
    private List<Transform> enemyList = new List<Transform>();

    void Detectenemies()
    {
        enemyList.Clear();

        foreach (var enemy in enemyList)
        {
            if (Vector3.Distance(transform.position, enemy.position) < 10.0f)
            {
                enemyList.Add(enemy);
            }
        }
    }

    ^자동으로 배열의 크기를 늘려주지만 4 -> 8칸 or 16 -> 32칸 늘어날 때 32칸에 16칸의 내용을 붙이는 복사비용이 발생한다
    List<int> list = new List<int>('50'); : 미리 50칸을 만들어둔다. 최대 사용할 범위를 지정. 복사비용 발생 x

    private void Update()
    {
        for (int i = 0; i < 200; i++)
        {
            list.Add(i);
            list.TrimExcess(); : 내부 배열을 실제 Count크기에 맞춰서 줄여주는 구문 // 더이상 큰 배열이 필요 없을 때
        }
    }

    드로우콜 감소
    material > Inspector > Enable GPU instancing
    오브젝트가 동일해야한다, 같은 머테리얼을 사용해야 한다
    */
}
