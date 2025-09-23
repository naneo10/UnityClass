using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float moveSpeed = 5.0f;
    //float moveX = 0.0f;
    //float moveZ = 0.0f;

    void Start()
    {
        //Debug.Log("Unity�� �°� ȯ���Ѵ�");
    }

    void Update()
    {
        //Debug.Log("������Ʈ ȣ��!");
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //transform.Translate(Vector3.forward * vertical * moveSpeed * Time.deltaTime);
        //transform.Translate(Vector3.right * horizontal * moveSpeed * Time.deltaTime);

        //FPS: 30(�ʴ� 30��)
        //�� ������ ���� X��ǥ�� 1���� -> 1�� �� 30ĭ ����
        //Time.deltaTime = 1/30 * FPS(30) * 1 = 1
        //FPS: 60(�ʴ� 60��)
        //�� ������ ���� X��ǥ�� 1���� -> 1�� �� 60ĭ ����
        //Time.deltaTime = 1/60 * FPS(60) * 1 = 1

        Vector3 moveDirection = new Vector3(horizontal, 0.0f, vertical).normalized;

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        //if (Input.GetKey(KeyCode.W))
        //{
        //    moveZ = 1.0f;
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    moveZ = -1.0f;
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    moveX = -1.0f;
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    moveX = 1.0f;
        //}

        //Vector3 moveDir = new Vector3(moveX, 0.0f, moveZ).normalized;

        //transform.Translate(moveDir * moveSpeed * Time.deltaTime);
    }
}
