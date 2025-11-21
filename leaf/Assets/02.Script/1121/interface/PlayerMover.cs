using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 10.0f;

    private Rigidbody rb;
    private Vector3 inputDir; //축

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        inputDir = new Vector3(h, 0.0f, v).normalized;

        //입력이 있을 때만 회전 처리 하겠다
        if (inputDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(inputDir);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);
        }
    }

    //물리니까 움직이는건 여기서
    private void FixedUpdate()
    {
        if (inputDir.sqrMagnitude > 0.01f)
        {
            Vector3 move = inputDir * moveSpeed * Time.fixedDeltaTime;

            rb.MovePosition(transform.position + move);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }
}