using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    //장애물용 레이어
    [SerializeField] private LayerMask m_obstacleLayer;

    //플레이어가 한 칸 이동할 때의 간격
    private float m_BoardSpacing = 1.0f;

    //이동경로를 보여줌
    private PlayerPath m_PlayerPath;

    //외부에서 읽기 전용으로
    public PlayerPath playerPath => m_PlayerPath;

    void Start()
    {
        m_PlayerPath = gameObject.GetComponent<PlayerPath>();
    }

    public void Move(Vector3 movement)
    {
        //현재위치 + 이동방향
        Vector3 destination = transform.position + movement;

        transform.position = destination;
    }

    public bool IsValidMove(Vector3 movement)
    {
        //이동 방향에 장애물이 있는지 확인
        return !Physics.Raycast(
            transform.position,
            movement,
            m_BoardSpacing,
            m_obstacleLayer
            );
    }
}
