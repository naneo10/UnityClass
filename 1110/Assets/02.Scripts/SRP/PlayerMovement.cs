using Unity.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float m_moveSpeed = 5.0f;
    [SerializeField] private float m_Acceleration = 10.0f;
    [SerializeField] private float m_deceleration = 6.0f;

    private float m_currentSpeed = 0.0f;
    private CharacterController m_characterController;

    private float m_InitYPos;
    private float m_speedMutiplier = 1.0f;

    public CharacterController CharacterController
    {
        get { return m_characterController; }
    }
    public float speedMutiplier
    {
        get { return m_speedMutiplier; }
        set { m_speedMutiplier = value; }
    }

    private void Awake()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    void Start()
    {
        m_InitYPos = transform.position.y;
    }

    public void Move(Vector3 InputVector)
    {
        if (InputVector == Vector3.zero)
        {
            if(m_currentSpeed > 0)
            {
                m_currentSpeed -= m_deceleration * Time.deltaTime;
                m_currentSpeed = Mathf.Max(m_currentSpeed, 0);
            }
        }
        else
        {
            m_currentSpeed = Mathf.Lerp(m_currentSpeed, m_moveSpeed, Time.deltaTime * m_moveSpeed * m_Acceleration);
        }

        Vector3 movement = m_currentSpeed * m_speedMutiplier * Time.deltaTime * InputVector.normalized;

        CharacterController.Move(movement);

        transform.position = new Vector3(transform.position.x, m_InitYPos, transform.position.z);
    }
}
