using UnityEngine;

public class FollowCam : MonoBehaviour
{
    #region field
    [Header("¼³Á¤")]
    [SerializeField] private Transform target;

    private float followSpeed = 5.0f;
    #endregion

    void LateUpdate()
    {
        Follow();
    }

    #region method
    private void Follow()
    {
        if (target == null) return;

        Vector3 targetPos = transform.position;

        targetPos.x = target.position.x;
        targetPos.y = target.position.y;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            followSpeed * Time.deltaTime
            );
    }
    #endregion
}
