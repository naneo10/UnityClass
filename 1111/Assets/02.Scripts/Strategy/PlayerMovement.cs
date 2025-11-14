using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    private CharacterController controller;
    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(moveX, 0.0f, moveY);

        if(move.magnitude > 1.0f)
        {
            move.Normalize();
        }

        controller.Move(move * moveSpeed * Time.deltaTime);
    }
}
