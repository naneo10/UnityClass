using UnityEngine;

public class PlayerControllerS : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(horizontal, 0.0f, vertical).normalized;

        transform.Translate(move * 5.0f * Time.deltaTime);
    }
}
