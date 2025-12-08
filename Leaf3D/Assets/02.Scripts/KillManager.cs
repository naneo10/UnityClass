using System.Collections.Generic;
using UnityEngine;

public class KillManager : MonoBehaviour
{
    public static KillManager Instance { get; private set; }
    [SerializeField] private PlayerController player;
    [SerializeField] private GameObject allyPrefab; //분신 프리펩

    [SerializeField] private int[] spawnThresholds = { 20, 50, 100 };
    [SerializeField] private float allySpacingX = 1.5f;

    private int killCount = 0;
    private int nextIndex = 0; //spawnTresholds의 다음 확인할 것 : 배열로 만들었기 때문에

    private List<Transform> allies = new List<Transform>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void AddKill()
    {
        killCount++;

        if (nextIndex < spawnThresholds.Length &&
            killCount >= spawnThresholds[nextIndex]) //현재 죽인 수가 20, 50, 100중에 도달했으면 소환해라
        {
            SpawnAlly();

            nextIndex++;
        }

    }

    private void SpawnAlly()
    {
        int allyIndex = allies.Count; //생성된 분신의 수

        int side = (allyIndex % 2 == 0) ? 1 : -1; //왼쪽 오른쪽 번갈아서 배치해야되기에

        int step = allyIndex / 2 + 1; //수치 변경해서 확인해볼 것

        float offsetX = allySpacingX * step * side;

        Vector3 spawnPos = player.transform.position + new Vector3(offsetX, 0, 0);

        Quaternion rot = player.transform.rotation;

        GameObject allyObj = Instantiate(allyPrefab, spawnPos, rot); //해당 위치랑 회전으로 생성

        AllyFollower follower = allyObj.GetComponent<AllyFollower>(); //만들어진 분신에 컴포넌트 불러오기

        follower.SetTarget(player.transform, new Vector3(offsetX, 0, 0));

        AllyShooter shooter = allyObj.GetComponent<AllyShooter>(); //스크립트 불러오고
        if (shooter != null) //있으면
        {
            shooter.SetPlayer(player); //슈터에게 플레이어 정보 전달
        }

        allies.Add(allyObj.transform); //생성된 분신을 해당 리스트에 추가
    }
}
