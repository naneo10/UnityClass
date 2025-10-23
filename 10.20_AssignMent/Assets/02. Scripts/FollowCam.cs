using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [Header("¼³Á¤")]
    public Transform target;

    [SerializeField] private Vector2 offset = new Vector2(0.0f, 1.0f);
    [SerializeField] private float followSpeed = 7.0f;

    void LateUpdate()
    {
        Vector3 targetPos = transform.position;

        targetPos.y = target.position.y;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime
            );
    }
}
