using UnityEngine;

public class ItemPick : MonoBehaviour
{
    [Header("magnetic")]
    [SerializeField] private float magnetRadius = 1.0f;
    [SerializeField] private float pullSpeed = 4.0f;
    [SerializeField] private float collectDistance = 0.3f;

    [Header("Layer")]
    [SerializeField] LayerMask itemLayer;

    [Header("CountPoint")]
    public int currentPoint = 0;

    [Header("Sound")]
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        PickUp();
    }

    private void PickUp()
    {
        Collider[] hits = Physics.OverlapSphere(
            transform.position,
            magnetRadius,
            itemLayer
            );

        for (int i = 0; i < hits.Length; i++)
        {
            if (!hits[i].TryGetComponent<ItemTypeA>(out ItemTypeA itemTypeA))
            {
                Debug.Log("None");
                continue;
            }

            float distance = Vector3.Distance(transform.position, itemTypeA.transform.position);

            if (distance <= collectDistance)
            {
                currentPoint = itemTypeA.GetPoint(currentPoint, itemTypeA.point);
                audioSource.Play();
                itemTypeA.ChangeColor(currentPoint);
            }

            if (currentPoint == 20)
            {
                GameManager.Instance.Clear();
            }

            itemTypeA.transform.position = Vector3.Lerp(
                itemTypeA.transform.position,
                transform.position,
                pullSpeed * Time.deltaTime
                );
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);
    }
}