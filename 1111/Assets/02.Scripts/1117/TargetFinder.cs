using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private float searchRadius = 20.0f;
    [SerializeField] private LayerMask targetLayerMask;

    //가비지 콜렉터 최적화 : 이 배열을 재사용 하겠다 미리 만들어 둔 것을
    private static readonly Collider[] s_CoolliderBuffer = new Collider[64];
    
    public Transform GetTarget()
    {
        Vector3 origin = transform.position;

        int hitCount = Physics.OverlapSphereNonAlloc( //새로 할당을 하지 않겠다
            origin,
            searchRadius,
            s_CoolliderBuffer,
            targetLayerMask
            );

        //매번 새로운 cols을 반환해 메모리 사용
        //예) Colllider[] cols = Physics.OverlapSphere(center, radius, layerMask);

        if (hitCount == 0) return null;

        Transform nearest = null;
        float bestDistance = float.PositiveInfinity; //무한대로 설정

        for (int i = 0; i < hitCount; i++)
        {
            Collider col = s_CoolliderBuffer[i];

            if (col == null) continue;

            Transform target = col.transform;

            float sqrDistance = (target.position - origin).sqrMagnitude;

            if (sqrDistance < bestDistance)
            {
                bestDistance = sqrDistance;
                nearest = target;
            }
        }

        return nearest;
    }
}
