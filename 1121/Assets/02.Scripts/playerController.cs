using UnityEngine;

public class playerController : MonoBehaviour
{

    private PlayerMovement movement;
    [Header("Marker")]
    [SerializeField] private GameObject markerPrefab;

    private GameObject markerInstance;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        if(markerPrefab != null)
        {
            markerInstance = Instantiate(markerPrefab);
            markerInstance.SetActive(false);
        }
    }
    

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                movement.MoveTo(hit.point);

                if(markerInstance!=null)
                {
                    Vector3 markerPos = hit.point;
                    markerPos.y += 0.01f;
                    markerInstance.transform.position = markerPos;
                    markerInstance.SetActive(true);
                }
            }
        }
    }

    public void HiddenMarker()
    {
        if(markerInstance!=null)
        {
            markerInstance.SetActive(false);    
        }
    }
}
