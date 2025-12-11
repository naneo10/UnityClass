using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PathFinder : MonoBehaviour
{
    GridManager gridManager;
    public Vector2Int start = new Vector2Int(0, 0);
    public Vector2Int end = new Vector2Int(9, 9);

    private int dfsCount = 0;
    private int bfsCount = 0;
    private int aStarCount = 0;

    [SerializeField] Text DFS_T, BFS_T, AStar_T;

    private void Awake()
    {
        gridManager = GetComponent<GridManager>();
    }

    #region DFS
    //-start에서 end까지의 경로를 반환해주는 녀석
    public List<Vector2Int> GetDFSPath()
    {
        HashSet<Vector2Int> visited = new HashSet<Vector2Int>();
        //start 위치에서 DFS를 시작한 다음 end까지의 경로를 찾는다.
        return DFS(start, end, visited);
    }

    //DFS (재귀)
    //HashSet : 중복 방지용 / current : 검사중인 위치
    List<Vector2Int> DFS(Vector2Int current, Vector2Int end, HashSet<Vector2Int> visited)
    {
        dfsCount++;
        //current가 맵 밖이거나 이미 방문한 칸이면 더 이상 진행하지 않는다.
        if (!IsValid(current) || visited.Contains(current))
        {
            return null;
        }
        //현재칸을 방문한 것으로 체크
        visited.Add(current);

        //current가 목표 위치라면
        //currnet 하나만 담긴 경로 리스트를 만들어서 반환
        if (current == end)
        {
            return new List<Vector2Int> { current };
        }

        //4방향으로 재귀적으로 뻗어나가면서 체크
        Vector2Int[] dirs =
        {
            Vector2Int.down,
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.right
        };
        //각 방향으로 한 칸씩 이동하면서 재귀호출

        foreach (var dir in dirs)
        {
            //다음 칸 위치 계산
            var next = current + dir;
            //next에서 end까지 경로를 재귀적으로 탐색
            var path = DFS(next, end, visited);
            if (path != null)
            {
                path.Insert(0, current);

                return path;
            }
        }
        DFS_T.text = "DFS : " + dfsCount.ToString();
        return null; //검색했는데도 길을 못찾으면 null을 반환
    }

    //해당 좌표가 이동 가능한 칸인지 검사
    bool IsValid(Vector2Int pos)
    {
        //그리드 매니저에서 2차원 배열 형태의 맵 데이터를 가져온다
        var grid = gridManager.gridData;
        var width = grid.GetLength(0);
        int height = grid.GetLength(1);

        return pos.x >= 0 && //왼쪽 벽 밖으로 나가지 않았다
               pos.x < width && //오른쪽 벽 밖으로 나가지 않았다
               pos.y >= 0 && //위쪽으로 벗어나지 않았다
               pos.y < height && //아래쪽으로 벗어나지 않았다
               grid[pos.y, pos.x] == 0; //해당 좌표가 이동 가능한 칸인지 확인
    }
    #endregion

    #region BFS
    public List<Vector2Int> GetBFSPath()
    {
        //이미 방문한 칸을 기록
        var visited = new HashSet<Vector2Int>();
        //각 칸이 어디에서 왔는지 기록, 경로 재구성
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        //BFS에서 사용할 큐
        var queue = new Queue<Vector2Int>();

        //ㅓ시작 지점을 큐에 넣고 방문처리
        queue.Enqueue(start);
        visited.Add(start);

        Vector2Int[] dirs =
        {
            Vector2Int.down,
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.right
        };

        //큐가 빌 때까지 반복
        while (queue.Count > 0)
        {
            bfsCount++;
            //큐에서 가장 먼저 들어온 위치를 꺼내자
            var current = queue.Dequeue();

            //목표 위치에 도달했다면
            if (current == end)
            {
                BFS_T.text = "BFS: " + bfsCount.ToString();
                //cameFrom을 이용해 경로를 역추적해서 반환
                return ReconstructPath(cameFrom, end);
            }

            //아직 목표가 아니라면 이웃 네 칸을 확인
            foreach (var dir in dirs)
            {
                var next = current + dir;
                //맵 밖이거나 이미 방문했던 칸이면 무시
                if (!IsValid(next) || visited.Contains(next))
                {
                    continue;
                }
                //방문 예정으로 큐에 넣고
                queue.Enqueue(next);
                //방문 체크
                visited.Add(next);
                //next 칸에 오기 직전 칸은 current라고 기록
                cameFrom[next] = current;
            }
        }
        //큐가 빌 때까지 end를 찾지 못했다면 경로가 없음
        return null;
    }

    //경로를 재구성 하는 매서드
    //cameForm 정보와, end 위치를 가지고
    //start -> end로 이어지는 실제 경로 리스트를 만든다.
    List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int end)
    {
        //최종 경로를 담을 리스트
        List<Vector2Int> path = new List<Vector2Int>();

        //end에서 시작해서 역으로 start까지 올라감
        var current = end;

        //cameFrom에 current 키가 존재하는 동안 반복
        while (cameFrom.ContainsKey(current))
        {
            //현재 위치를 경로에 추가
            path.Add(current);

            //한 단계 이전 위치로 이동
            current = cameFrom[current];
        }
        //마지막으로 시작위치도 경로에 포함
        path.Add(current);

        //start -> end 순서로 만들어준다
        path.Reverse();

        return path;
    }
    #endregion

    #region A*
    public List<Vector2Int> GetAStarPath()
    {
        var openSet = new PriorityQueue<Vector2Int>();

        //재구성용
        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();

        var gScore = new Dictionary<Vector2Int, int>();
        var fScore = new Dictionary<Vector2Int, int>();

        openSet.Enqueue(start, 0);
        gScore[start] = 0;
        fScore[start] = Heuristic(start, end); //목표까지 추정 비용

        var closedSet = new HashSet<Vector2Int>();

        Vector2Int[] dirs =
        {
            Vector2Int.down,
            Vector2Int.up,
            Vector2Int.left,
            Vector2Int.right
        };
        
        while (openSet.Count > 0)
        {
            aStarCount++;
            var current = openSet.Dequeue();

            //목표 위치에 도달 했다면
            if (current == end)
            {
                AStar_T.text = "A* : " + aStarCount.ToString();
                return ReconstructPath(cameFrom, end);
            }

            closedSet.Add(current);

            foreach ( var dir in dirs)
            {
                var neighbor = current + dir;
                if (!IsValid(neighbor) || closedSet.Contains(neighbor))
                {
                    continue;
                }

                int tentativeG = gScore[current] + 1;

                //더 짧은 위치가 있다면 갱신
                if (!gScore.ContainsKey(neighbor) || tentativeG < gScore[neighbor])
                {
                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeG; //시작위치 부터 갱신
                    fScore[neighbor] = tentativeG + Heuristic(neighbor, end);

                    openSet.Enqueue(neighbor, fScore[neighbor]); //우선순위와 함께 오픈셋에 추가
                }
            }
        }

        return null;
    }

    int Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }
    #endregion
}
