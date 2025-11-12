using System.Collections.Generic;
using UnityEngine;

public class PlayerPath : MonoBehaviour
{
    [Header("Prefab & Ref")]
    [SerializeField] private GameObject pathPointPrefab; //이동경로에 찍을 프리팹
    [SerializeField] private Transform pathTransform; //생성된 점들을 담아둘 오브젝트
    [SerializeField] private Vector3 offset;
    [SerializeField] private LineRenderer lineRenderer; //점들을 선으로 이어주는 컴포넌트
    [SerializeField] private List<Vector3> pointList = new List<Vector3>();

    //경로 점들을 저장해두는 스택
    private Stack<GameObject> pathObjects = new Stack<GameObject>();

    void Start()
    {
        AddToPath(transform.position);
    }

    void Update()
    {
        if (pointList.Count > 0)
        {
            lineRenderer.SetPositions(pointList.ToArray());
        }
    }

    //플레이어가 이동할 때 마다 새로운 점을 찍는 메서드
    public void AddToPath(Vector3 position)
    {
        //프리팹이 설정되지 않았으면 중단
        if (pathPointPrefab == null) return;

        //점은 현재 + offset 위치에 생성
        GameObject newPathObject = Instantiate(pathPointPrefab, position + offset, Quaternion.identity);

        //나중에 삭제하거나 되돌릴 수 있도록 스택에 저장
        pathObjects.Push(newPathObject);
        if (pathTransform != null)
        {
            newPathObject.transform.parent = pathTransform;
        }
        //라인렌더러에 표시할 점 목록 업데이트
        UpdatePointList();
    }

    //경로의 마지막 점을 삭제하는 메서드
    public void RemoveFromPath()
    {
        if (pathObjects.Count == 0) return;

        GameObject lastObject = pathObjects.Pop();
        Destroy(lastObject.gameObject);
        UpdatePointList();
    }

    //라인렌더러에 표시할 점 좌표목록을 새로 갱신하는 메서드
    private void UpdatePointList()
    {
        //기존 리스트 비우고 새로 채우자
        pointList.Clear();
        foreach (GameObject obj in pathObjects)
        {
            if (obj != null)
            {
                pointList.Add(obj.transform.position);
            }
        }
        //라인렌더러의 점 개수 갱신
        lineRenderer.positionCount = pointList.Count;
    }
}
