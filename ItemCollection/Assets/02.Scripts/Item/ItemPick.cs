using UnityEngine;

public class ItemPick : MonoBehaviour
{
    [Header("magnetic")]
    [SerializeField] private float magnetRadius = 5.0f;
    [SerializeField] private float pullSpeed = 8.0f;
    [SerializeField] private float collectDistance = 0.3f;

    [Header("Layer")]
    [SerializeField] LayerMask itemLayer;

    void Update()
    {
        
    }


}
