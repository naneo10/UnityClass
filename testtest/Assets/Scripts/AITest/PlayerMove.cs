using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMove : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 5.0f;

    private CharacterController controller;
    private Animator animator;

    private void Awake()
    {
       controller =  GetComponent<CharacterController>();   
        animator = GetComponent<Animator>();    
    }
    

    void Update()
    {
        //키 입력받고
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        //방향 만들고


        Vector3 inputDir = new Vector3(h, 0.0f, v);
        inputDir = inputDir.normalized;
        //방향설정하고
        if(inputDir.sqrMagnitude>0.0f)
        {
            transform.forward = inputDir;
        }
        //이동시키고
        Vector3 move = inputDir * moveSpeed;
        controller.Move(move * Time.deltaTime);
        //애니메이션 Speed값 전달

        float speedValue = inputDir.magnitude;

        animator.SetFloat("Speed", speedValue);

    }
}
