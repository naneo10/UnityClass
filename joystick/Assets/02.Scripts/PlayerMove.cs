using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region field
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

    private Animator anim;

    private static readonly int moveHash = Animator.StringToHash("Move");
    #endregion

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    #region method
    public void MoveHandler(Vector2 inputDirection)
    {
        Vector3 direction = new Vector3(inputDirection.x, 0.0f, inputDirection.y);

        if (direction.magnitude > 1.0f)
        {
            direction = direction.normalized;
        }

        Move(direction);
    }

    private void Move(Vector3 direction)
    {
        Vector3 move = direction * moveSpeed * Time.deltaTime;
        transform.position += move;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                Time.deltaTime * rotationSpeed
                );
        }

        anim.SetFloat(moveHash, direction.magnitude);
    }
    #endregion
}
