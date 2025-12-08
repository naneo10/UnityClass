using UnityEngine;

public class AllyFollower : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float followSpeed = 10.0f;

    public void SetTarget(Transform target, Vector3 offset)
    {
        this.target = target;
        this.offset = offset;
    }

    void Update()
    {
        if (target == null) return;

        //따라갈 위치 계산
        Vector3 targetPos = target.position + offset;

        targetPos.y = transform.position.y; //y축 초기화

        transform.position = Vector3.Lerp(transform.position, targetPos, followSpeed * Time.deltaTime);

        transform.rotation = target.rotation;
    }
}
