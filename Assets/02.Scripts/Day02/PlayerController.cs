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
        rb = GetComponent<Rigidbody>();//캐싱
    }

    void Update()
    {
        //rb = GetComponent<Rigidbody>(); //매 프레임마다 불러오기 때문에 성능에 영향을 준다 이렇게 작성하지 않도록 해야 한다

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
        Debug.Log("OnCollisionEnter 호출");

        //if(collision.gameObject.tag=="Wall") ///권장되지 않는 방법
        //{
        //    Debug.Log("벽에 부딪힘");
        //}

        if(collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("벽에 부딪힘");
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay 호출");
    }
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit 호출");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter 호출");
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("OnTriggerExit 호출");
    }
}
