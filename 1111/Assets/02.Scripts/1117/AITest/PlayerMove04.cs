using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove04 : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rotateSpeed = 10.0f;
    [SerializeField] private float smoothInputSpeed = 10.0f;


    private Animator animator;
    private Vector3 smoothInput = Vector3.zero;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(h, 0.0f, v).normalized;

        smoothInput = Vector3.Lerp(smoothInput, inputDir, smoothInputSpeed * Time.deltaTime);


        if (smoothInput.sqrMagnitude > 0.01f)
        {
            Quaternion targetRot = Quaternion.LookRotation(smoothInput);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * rotateSpeed);
        }


        transform.Translate(
          smoothInput * moveSpeed * Time.deltaTime,
          Space.World
      );

        animator.SetFloat("Speed", smoothInput.magnitude);

    }
}
