using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Rigid
    private float moveSpeed = 5.0f;
    private float horizontal;
    private float vertical;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();//ĳ��
    }

    void Update()
    {
        //rb = GetComponent<Rigidbody>(); //�� �����Ӹ��� �ҷ����� ������ ���ɿ� ������ �ش� �̷��� �ۼ����� �ʵ��� �ؾ� �Ѵ�

        //if(Input.GetKey(KeyCode.W))
        //{
        //    transform.Translate(new Vector3(0.0f, 0.0f, 5.0f * Time.deltaTime));
        //}
        //if(Input.GetKey(KeyCode.S))
        //{
        //    transform.Translate(new Vector3(0.0f, 0.0f, -5.0f * Time.deltaTime));
        //}

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        Vector3 move = new Vector3(horizontal, 0.0f, vertical).normalized;

        rb.velocity = move * moveSpeed + new Vector3(0.0f, rb.velocity.y, 0.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter ȣ��");

        //if(collision.gameObject.tag=="Wall") ///������� �ʴ� ���
        //{
        //    Debug.Log("���� �ε���");
        //}

        if(collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("���� �ε���");
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay ȣ��");
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit ȣ��");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter ȣ��");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit ȣ��");
    }
}
