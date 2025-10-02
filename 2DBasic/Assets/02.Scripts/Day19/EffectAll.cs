using Unity.VisualScripting;
using UnityEngine;

public class EffectAll : MonoBehaviour
{
    //내부 참조
    //PlayerAnimation playerAnimation; //C# 처럼 인스턴스화 안됨?
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    public Transform transform;

    //이펙트
    private bool isAvoid;

    private float inputX;

    /*
    private bool inputA;
    private bool inputD;
    */ //방향

    //해시
    private static readonly int avoidEffectHash = Animator.StringToHash("isAvoidEffect");

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        transform = GetComponent<Transform>();
    }

    void Update()
    {
        /*
        NullReferenceException: Object reference not set to an instance of an object
        EffectAll.Update () (at Assets/02.Scripts/Day19/EffectAll.cs:19)
        개체 참조가 개체의 인스턴스로 설정되지 않았습니다.
        */
        //PlayerAnimation playerAnimation = new PlayerAnimation(); //UNT0010

        /*
        if (Input.GetKey(KeyCode.A))
        {
            inputA = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputD = true;
        }
        */ //방향 전환이 한 텀 지연됨

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isAvoid = true;
        }
        
        inputX = Input.GetAxisRaw("Horizontal");
        if (inputX != 0)
        {
            if (inputX < 0)
            {
                //transform.position = new Vector2(, transform.position.y);
                spriteRenderer.flipX = true;
                Debug.Log("A 확인");
            }
            else
            {
                //transform.position = new Vector2(transform.position.x, transform.position.y);
                spriteRenderer.flipX = false;
                Debug.Log("D 확인");
            }
        }

    }

    private void FixedUpdate()
    {
        AvoidEffect();
    }

    private void AvoidEffect()
    {
        if (isAvoid)
        {
            anim.SetBool(avoidEffectHash, true);
            isAvoid = false;
        }
        else if (!isAvoid)
        {
            anim.SetBool(avoidEffectHash, false);
        }
    }
}
