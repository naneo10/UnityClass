using UnityEngine;
using UnityEngine.EventSystems; //키보드, 마우스, 터치를 이벤트로 오브젝트에 보낼 수 있는 기능을 지원

public class virtualJoyStick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(10, 150)] private float leverRange;

    private Vector2 inputDirection;
    private bool isInput;

    [SerializeField] private PlayerMove controller;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (isInput)
        {
            InputControlVector();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    //오브젝트를 클릭해서 드래그 하는 도중에 들어오는 이벤트
    //하지만 클릭을 유지한 상태로 마우스를 멈추면 이벤트가 들어오지 않음
    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
        controller.MoveHandler(Vector2.zero);
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;

        //inputVector가 해상도를 기반으로 만들어진 값이라 캐릭터의 이동속도로 쓰기엔 큰 값을 가지고 있음
        //화면 해상도를 기준으로 값이 달라지기 때문에 정규환된 값을 사용해야 한다
        inputDirection = inputVector / leverRange; //0 ~ 1 사이 값으로 정규화를해 캐릭터로 넘겨줌
    }

    void InputControlVector()
    {
        //캐릭터에게 입력벡터를 전달
        controller.MoveHandler(inputDirection);
        //Debug.Log(inputDirection.x + " / " + inputDirection.y);
    }
}