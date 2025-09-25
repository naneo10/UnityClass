using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController_A : MonoBehaviour
{
    private Rigidbody playerRigid;
    private float moveSpeed = 5.0f;

    private float horizontal;
    private float vertical;

    private void Awake()
    {
        playerRigid = GetComponent<Rigidbody>();
    }

    private void Start()
    {
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }

    void PlayerMove()
    {
        Vector3 newVelocity = new Vector3(horizontal * moveSpeed, 0.0f, vertical * moveSpeed);
        playerRigid.velocity = Vector3.Lerp(playerRigid.velocity, newVelocity, Time.deltaTime * 10.0f);

        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(newVelocity);

        if (horizontal == 0 && vertical == 0) return;

        transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, Time.deltaTime * moveSpeed);
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }
}
