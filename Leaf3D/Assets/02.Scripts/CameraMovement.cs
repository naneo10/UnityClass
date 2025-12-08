using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Transform target;

    void Update()
    {
        float lerpX = Mathf.Lerp(transform.position.x, target.position.x, speed * Time.deltaTime);
        transform.position = new Vector3(lerpX, transform.position.y, transform.position.z);
    }
}
