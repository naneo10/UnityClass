using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{
    [SerializeField] float speed; //배경이 내려가는 속도
    [SerializeField] int startIndex; //현재 맨 위에 있는 스프라이트 인덱스
    [SerializeField] int endIndex; //맨 아래에 있는 스프라이트 인덱스

    public Transform[] sprites; //배경 스프라이트들을 담아둘 배열
    private float viewHeight; //화면 높이 (카메라 기준)
    void Start()
    {
        //카메라의 세로 크기를 계산해서 화면 높이를 구함.
        viewHeight = Camera.main.orthographicSize * 2;
    }

    void Update()
    {
        //화면을 아래로 이동
        transform.position += Vector3.down * speed * Time.deltaTime;

        //endIndex에 있는 스프라이트가 화면 밑으로 완전히 벗어나면
        if (sprites[endIndex].position.y < -viewHeight)
        {
            sprites[endIndex].localPosition = sprites[startIndex].localPosition + Vector3.up * viewHeight;

            //startIndex와 endIndex를 서로 바꿔준다
            int temp = startIndex;
            startIndex = endIndex;
            endIndex = temp;

            //튜플 (startIndex, endIndex) = (endIndex, startIndex); 위의 식과 동일
        }
    }
}
