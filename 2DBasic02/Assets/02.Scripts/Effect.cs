using System.Collections;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private Animator anim; //이펙트 애니메이션을 재생할 컴포넌트

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //이펙트를 재생하는 메서드
    public void PlayEffect()
    {
        //오브젝트 활성화
        gameObject.SetActive(true);

        //재생
        //애니메이션 재생
        anim.Play("Effect");

        StartCoroutine(DisableAnimationCo());

        //애니메이션 종료
    }
    IEnumerator DisableAnimationCo()
    {
        //GetCurrentAnimatorStateInfo: 현재 애니메이션 상태 정보
        //현재 애니메이션의 재생 길이만큼 기다려라
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length); //.length 애니메이션의 총 재생 시간

        //애니메이션이 끝나면 풀에 다시 반환하자
        Managers.Pool.ReturnPool(this);
    }
}
