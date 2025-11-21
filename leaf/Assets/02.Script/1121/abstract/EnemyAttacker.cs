using UnityEngine;

public class EnemyAttacker : MonoBehaviour
{
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if ( Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                EnemyBase enemy = hit.collider.GetComponent<EnemyBase>();

                if (enemy != null)
                {
                    enemy.Attack();
                }
            }
        }
    }
}
