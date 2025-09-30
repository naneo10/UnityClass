using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{
    /*
    [sorting Layer]
    -말 그대로 무엇이 앞에 보이고 무엇이 뒤에 보일 것인가?
    -게임화면은 평면(2D)위에 여러 스프라이트나 UI가 겹쳐 보일 수 있음
    -이때 Z좌표가 같아도 어떤 그림이 앞에 나올지 정해야 한다.
    
    [pixel per Unit]
    -스프라이트 이미지 몇 픽셀을 유니티 월드에서 1유닛으로 볼 것인가?
    -스프라이트 이미지는 픽셀단위(pixel)
    -유니티는 픽셀과 유닛의 변환 기준이 필요하다.
    */
    [Header("설정")]
    public Transform camera;
    [SerializeField] float scrollingSpeed = 0.0f;
    [SerializeField] int spriteCount = 2;

    private Vector2 prevCamPos; //이전 카메라 위치
    private float spriteWidth; //이미지 폭 계산

    void Start()
    {
        if (!camera) camera = Camera.main.transform;

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        if(spriteRenderer != null )
        {
            //월드 단위에서 폭을 계산
            spriteWidth = spriteRenderer.bounds.size.x;
        }
    }

    void LateUpdate()
    {
        //카메라 이동량 계산
        Vector2 delta = (Vector2)camera.position - prevCamPos;

        //이동
        transform.position += new Vector3(delta.x * scrollingSpeed, 0.0f, 0.0f);

        //이전 카메라 위치 저장
        prevCamPos = camera.position;

        //공식 카메라가 보는 화면의 왼쪽 X좌표의 위치
        float leftEdge = camera.position.x - Camera.main.orthographicSize * Camera.main.aspect;

        if (transform.position.x + spriteWidth * 0.5f < leftEdge)
        {
            transform.position += Vector3.right * (spriteWidth * spriteCount);
        }
    }
}
