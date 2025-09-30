using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigid;
    [SerializeField] private float MoveSpeed = 8.0f; //어트리뷰트 캡슐화를 유지하면서 내부에 보여줄 떄

    private float horizontal;
    private float vertical;

    void Awake()
    {
        playerRigid = GetComponent<Rigidbody>();

    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        //if(Input.GetKey(KeyCode.UpArrow) == true)
        //{
        //    playerRigid.AddForce(0.0f, 0.0f, speed);
        //}
        //if(Input.GetKey(KeyCode.DownArrow) == true)
        //{
        //    playerRigid.AddForce(0.0f, 0.0f, -speed);
        //}
        //if(Input.GetKey(KeyCode.RightArrow) == true)
        //{
        //    playerRigid.AddForce(speed, 0.0f, 0.0f);
        //}
        //if(Input.GetKey(KeyCode.LeftArrow) == true)
        //{
        //    playerRigid.AddForce(-speed, 0.0f, 0.0f);
        //}
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        Vector3 newVelocity = new Vector3(horizontal * MoveSpeed, 0.0f, vertical * MoveSpeed);
        playerRigid.velocity = Vector3.Lerp(playerRigid.velocity, newVelocity, Time.deltaTime * 10.0f);
    }

    public void Die()
    {
        gameObject.SetActive(false);

        GameManager gameManager = FindObjectOfType<GameManager>();
        gameManager.EndGame();
    }
}
