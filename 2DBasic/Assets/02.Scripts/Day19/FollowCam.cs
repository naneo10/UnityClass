using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Header("설정")]
    public Transform target;

    [SerializeField] private Vector2 offset = new Vector2(0.0f, 1.0f);
    [SerializeField] private float followSpeed = 5.0f;
    private bool followX = true;

    private void LateUpdate()
    {
        if (!target) return;

        //목표 값 현재 카메라 위치
        Vector3 targetPos = transform.position;

        if (followX)
        {
            targetPos.x = target.position.x + offset.x;
        }

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime
            );
    }
}
