using System.Collections.Generic;

/*
Astar : A*
휴리스틱을을 사용하여 최적의 경로를 찾는 알고리즘
-목표지점 까지의 예상비용을 계산하여 최단경로 + 빠른 탐색을 동시에 달성
f(n) = g(n) + h(n)
g: 시작점에서 현재 노드까지의 실제 비용(실제 이동거리)
h: 현재 노드에서 목표노드까지의 예상비용(예상거리, 휴리스틱 비용)
f: 현재 노드까지의 총 비용(실제 이동거리 + 예상거리) / 여기에서 우선순위 Queue가 사용이 된다

f가 가장 낮은 노드부터 탐색
1.시작노드부터 탐색을 시작
2.현재 노드에서 인접한 노드들을 확인
3.인접한 노드들의 f 값을 계산
4.f 값이 가장 낮은 노드를 선택
5.선택한 노드가 목표노드인지 확인
6.목표노드가 아니면 다시 2번으로 돌아가서 반복

휴리스틱이란?
목표까지 얼마나 남았는지 대충 예측해주는 함수 : 빠르게 길을 찾기 위한 녀석
*/
public class PriorityQueue<T>
{
    //힙 구조로 만드는게 더 빠르다 : 지금은 테스트이기 때문에 리스트로 제작
    //내부에서 실제 데이터를 담아둘 리스트
    //List<(T item, int priority)>
    //리스트의 한 칸에 (아이템, 우선순위)
    private List<(T item, int priority)> elements = new List<(T item, int priority)>();

    public int Count => elements.Count;

    public void Enqueue(T item, int priority)
    {
        elements.Add((item, priority));
    }

    public T Dequeue()
    {
        int bestIndex = 0;
        
        for (int i = 1; i < elements.Count; i++)
        {
            if (elements[i].priority < elements[bestIndex].priority)
            {
                bestIndex = i;
            }
        }

        T bestItem = elements[bestIndex].item;

        elements.RemoveAt(bestIndex);

        return bestItem;
    }
}
