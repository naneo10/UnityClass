using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float detectRange = 5.0f;
    [SerializeField] private float stopRange = 1.5f;



    private Transform player;
    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();   
    }
    void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null )
        {
            player = playerObj.transform;
        }
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector3.Distance(transform.position, player.position);


        //범위 안에 들어 왔으면 추격
        if(dist<detectRange)
        {
            agent.SetDestination(player.position);


            if(dist<stopRange)
            {
                agent.ResetPath();  
            }
        }
        //멀리 있으면
        else
        {
            if(!agent.isStopped)
            {
                agent.ResetPath();
            }
        }
    }
}
