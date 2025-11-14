using System;
using UnityEngine;

[RequireComponent (typeof(Collider))] //자동으로 콜라이더를 붙여준다
public class ButtonSubject : MonoBehaviour
{
    public event Action Clicked;
    private Collider m_Collider;

    void Start()
    {
        m_Collider = GetComponent<Collider>();
    }

    void Update()
    {
        CheckCollider();
    }

    private void CheckCollider()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            if (Physics.Raycast (ray, out hitInfo, 100.0f))
            {
                if (hitInfo.collider == m_Collider)
                {
                    ClickButton();
                }
            }
        }
    }

    public void ClickButton()
    {
        Clicked?.Invoke();
    }
}
