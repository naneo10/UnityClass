using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [Header("key Controls")]
    [SerializeField] private KeyCode m_ForwardKey = KeyCode.W;
    [SerializeField] private KeyCode m_BackKey = KeyCode.S;
    [SerializeField] private KeyCode m_LeftKey = KeyCode.A;
    [SerializeField] private KeyCode m_RightKey = KeyCode.D;
    [SerializeField] private KeyCode m_UndoKey = KeyCode.U;
    [SerializeField] private KeyCode m_RedoKey = KeyCode.R;

    [SerializeField] private PlayerMover m_Player;

    private void Awake()
    {
        if (m_Player == null)
        {
            m_Player = GetComponent<PlayerMover>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(m_ForwardKey)) OnForwardInput();
        if (Input.GetKeyDown(m_BackKey)) OnBackInput();
        if (Input.GetKeyDown(m_LeftKey)) OnLeftInput();
        if (Input.GetKeyDown(m_RightKey)) OnRightInput();
        if (Input.GetKeyDown(m_UndoKey)) OnUndoInput();
        if (Input.GetKeyDown(m_RedoKey)) OnRedoInput();
    }

    private void RunPlayerCommand(PlayerMover playerMover, Vector3 movement)
    {
        if (playerMover == null) return;
        
        //움직일 수 있는 방향인지 확인
        if (playerMover.IsValidMove(movement))
        {
            ICommand command = new MoveCommand(playerMover, movement);

            //명령실행
            CommandInvoker.ExecuteCommand(command);
        }
    }

    //방향키 입력에 따라 호출되는 메서드
    private void OnLeftInput()
    {
        RunPlayerCommand(m_Player, Vector3.left);
    }

    private void OnRightInput()
    {
        RunPlayerCommand(m_Player, Vector3.right);
    }

    private void OnForwardInput()
    {
        RunPlayerCommand(m_Player, Vector3.forward);
    }

    private void OnBackInput()
    {
        RunPlayerCommand(m_Player, Vector3.back);
    }

    private void OnUndoInput()
    {
        CommandInvoker.UndoCommand();
    }

    private void OnRedoInput()
    {
        CommandInvoker.RedoCommand();
    }
}
