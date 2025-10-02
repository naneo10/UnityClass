using UnityEngine;

//적 이동 담당 스크립트
public class EnemyMovement : MonoBehaviour
{
    public enum MovePattern { Down, ZigZag}
    public MovePattern movePattern { get; private set; }

    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float zDistance = 2.0f;
    private float timeElapsed = 0.0f;

    public void SetRandomPattern()
    {
        movePattern = (MovePattern)Random.Range(0, System.Enum.GetValues(typeof(MovePattern)).Length);
    }

    public void ResetMoveMent()
    {
        timeElapsed = 0.0f;
    }

    public void MoveEnemy(Vector2 initSpawnPosition)
    {
        timeElapsed += Time.deltaTime;

        switch (movePattern)
        {
            case MovePattern.Down:
                transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
                break;
            case MovePattern.ZigZag:
                float zigzagX = Mathf.PingPong(timeElapsed * moveSpeed, zDistance * 2.0f) - zDistance;
                /*
                Mathf.PingPong(t, Length)
                입력 t가 커질수록 0~L까지 갔다가 다시 0으로 돌아오는 값을 반복해서 돌려줌
                L = dist * 2 D->zDistance
                범위는 0~2D
                마지막에 - D
                0 ~2D-> - D + D 로 중심을 0에 맞추기 위해서

                D = 2, L = 4
                0-> 1-> 2-> 3-> 4-> 3-> 2-> 1
                - 2
                - 2-> - 1-> 0-> 1-> 2
                */ //위의 식 정리

                //x축 : 처음 위치부터 좌우로 흔들
                //y축 : 계속 내려옴
                transform.position = new Vector2(
                    initSpawnPosition.x + zigzagX,
                    initSpawnPosition.y - timeElapsed * moveSpeed);
                break;
        }
    }
}
