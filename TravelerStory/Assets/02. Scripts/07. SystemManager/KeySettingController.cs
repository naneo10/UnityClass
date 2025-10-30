using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeySettingController : MonoBehaviour
{
    private KeyCode mOriginKeyCode;
    [SerializeField] private string mKeyBindingName;

    //키 설정 버튼
    //현재 할당된 키와 그 키를 수정할 수 있게 하는 버튼의 이미지
    [SerializeField] private Image mKeyButtonImage;
    //키 수정 버튼의 색상 변경을 수행하는 코루틴을 담는 변수
    private Coroutine mKeyButtonColorCor;

    //버튼 텍스트
    [SerializeField] private TextMeshProUGUI mKeyButtonText; //버튼의 하위 자식의 텍스트 필드

    //키 설정 옵션이 활성화 되는 경우 현재 슬롯에 맞는 키를 가져오고 텍스트로 표시합니다.
    private void OnEnable()
    {
        mOriginKeyCode = KeyManager.Instance.GetKeyCode(mKeyBindingName);
        mKeyButtonText.text = ((char)mOriginKeyCode).ToString().ToUpper();
    }

    //UI로 부터 호출되며 해당 슬롯에 키 설정을 시도합니다.
    public void BTN_ModifyKey()
    {
        mKeyButtonText.text = "< >";

        StartCoroutine(CorAssignKey());
    }

    //코루틴을 이용하여 키 입력을 기다리고, 키 입력을 받은 경우 키 유효성 검사를 합니다.
    private IEnumerator CorAssignKey()
    {
        while (true)
        {
            if (Input.anyKeyDown)
            {
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(kcode))
                    {
                        //기존의 코루틴 제거
                        if (mKeyButtonColorCor != null) { StopCoroutine(mKeyButtonColorCor); }

                        //키 설정을 할 수 있는 경우?
                        if (KeyManager.Instance.CheckKey(kcode, mOriginKeyCode))
                        {
                            //키 지정
                            KeyManager.Instance.AssignKey(kcode, mKeyBindingName);
                            mOriginKeyCode = kcode;

                            //키 레이블 변경
                            mKeyButtonText.text = ((char)kcode).ToString().ToUpper();

                            //녹색으로 설정 완료됨을 연출
                            mKeyButtonColorCor = StartCoroutine(CorChangeButtonColor(Color.green));
                        }
                        else
                        {
                            //키 레이블 변경
                            mKeyButtonText.text = ((char)mOriginKeyCode).ToString().ToUpper();

                            //빨간색으로 설정 완료됨을 연출
                            mKeyButtonColorCor = StartCoroutine(CorChangeButtonColor(Color.red));
                        }
                    }
                }
                yield break;
            }
            yield return null;
        }
    }

    private IEnumerator CorChangeButtonColor(Color targetColor, float colorSpeed = 2.0f)
    {
        float progress = 0;

        //targetColor로 변경
        while (true)
        {
            mKeyButtonImage.color = Color.Lerp(mKeyButtonImage.color, targetColor, progress);
            progress += colorSpeed * Time.deltaTime;

            //progress가 1이면 > 보관 완료
            if (progress > 1)
            {
                progress = 0;

                //targetColor에서 다시 돌아오기
                while (true)
                {
                    mKeyButtonImage.color = Color.Lerp(mKeyButtonImage.color, Color.white, progress);
                    progress += colorSpeed * Time.deltaTime;

                    //색상 전환 완료
                    if (progress > 1)
                    {
                        yield break;
                    }
                    yield return null;
                }
            }
            yield return null;
        }
    }
}
