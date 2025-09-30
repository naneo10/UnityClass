using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxColliderA : MonoBehaviour
{
    private void Awake()
    {
        
    }
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerA playerA))
            {
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent(out PlayerA playerA))
            {
                
            }
        }
    }
}
