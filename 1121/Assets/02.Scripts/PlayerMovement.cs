using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{


    [SerializeField] private float moveSpeed = 5.0f;

    private NavMeshAgent navMeshAgent;
    private playerController playerController;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();    
        playerController = GetComponent<playerController>();    
    }

    public void MoveTo(Vector3 position)
    {

        StopCoroutine("MoveCo");

        navMeshAgent.speed = moveSpeed;
        navMeshAgent.SetDestination(position);

        StartCoroutine("MoveCo");
    }

    IEnumerator MoveCo()
    {
        while (true) 
        {
            if(Vector3.Distance(navMeshAgent.destination, transform.position)<0.1f)
            {
                transform.position =  navMeshAgent.destination;
                navMeshAgent.ResetPath();

                if(playerController !=null)
                {
                    playerController.HiddenMarker();
                }
                break;
            }
            yield return null;
        }
    }
}
