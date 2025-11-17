using UnityEngine;

public class FollowCam : MonoBehaviour
{
    #region field
    public Transform target;

    [SerializeField] private Vector3 offset = new Vector3(0.0f, 6.0f, -4.0f);
    public float moveSpeed = 7.0f;
    #endregion

    void LateUpdate()
    {
        Follow();
    }

    #region method
    private void Follow()
    {
        if (!target) return;

        Vector3 targetPos = target.position;

        targetPos.x = target.position.x + offset.x;
        targetPos.y = target.position.y + offset.y;
        targetPos.z = target.position.z + offset.z;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            moveSpeed * Time.deltaTime
            );
    }
    #endregion
}