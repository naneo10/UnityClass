using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask m_ObstacleLayer;

    private PlayerInput m_playerInput;
    private PlayerMovement m_PlayerMovement;
    private PlayerAudio m_playerAudio;
    private PlayerFX m_playerFX;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_playerInput = GetComponent<PlayerInput>();
        m_PlayerMovement = GetComponent<PlayerMovement>();
        m_playerAudio = GetComponent<PlayerAudio>();
        m_playerFX = GetComponent<PlayerFX>();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (LayerMaskUtils.IsInLayer(hit.gameObject, m_ObstacleLayer))
        {
            if (m_playerAudio != null)
            {
                m_playerAudio.PlayRandomClip();
            }
            if (m_playerFX != null)
            {
                m_playerFX.PlayerEffect();
            }
        }
    }

    private void LateUpdate()
    {
        Vector3 inputVector = m_playerInput.InputVector;
        m_PlayerMovement.Move(inputVector);
    }
}
