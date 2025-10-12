using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Header("¼³Á¤")]
    public Transform target;

    [SerializeField] private Vector2 offset = new Vector2(0.0f, 3.0f);
    [SerializeField] private float followSpeed = 5.0f;

    private void LateUpdate()
    {
        if (!target) return;

        Vector3 targetPos = transform.position;

        targetPos.x = target.position.x + offset.x;
        targetPos.y = target.position.y + offset.y;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime
            );
    }
}
