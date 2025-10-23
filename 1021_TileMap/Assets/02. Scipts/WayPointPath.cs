using UnityEngine;

public class WayPointPath : MonoBehaviour
{
    //경로의 각점을 나타내는 트랜스폼 배열
    public Transform[] points;

    //경로를 반환하는 메서드
    public Transform[] GetPath()
    {
        //설정된 경로 반환
        return points;
    }

    private void OnDrawGizmos()
    {
        if (points == null) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < points.Length - 1; i++)
        {
            Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }
    }

}
