using UnityEngine;
/*
Rigidbody : https://dalbitdorong.tistory.com/13
�ε巴�� ȸ�� : https://dallcom-forever2620.tistory.com/3
������Ʈ ȸ�� ��� : https://coding-shop.tistory.com/248
Quaternion.Slerp : https://code-piggy.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-QuaternionSlerp#google_vignette
�ü� ���� ���� : https://blog.naver.com/gold_metal/220495685382
*/
public class BoxMove : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    private float horizontal;
    private float vertical;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

    }

    private void FixedUpdate()
    {
        //�̵�
        Vector3 move = new Vector3(horizontal, 0.0f, vertical).normalized;
        rb.velocity = move * moveSpeed;

        //ȸ��
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(move);

        if (horizontal == 0 && vertical == 0)
            return;

        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * moveSpeed);
    }
}
