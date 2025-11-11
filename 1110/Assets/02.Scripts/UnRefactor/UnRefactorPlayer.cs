using UnityEngine;

public class UnRefactorPlayer : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float acceleration = 10.0f;
    [SerializeField] private float deceleation = 5.0f;

    [Header("Controls")]
    [SerializeField] private KeyCode forawardKey = KeyCode.W;
    [SerializeField] private KeyCode backWardKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;

    [Header("Collision")]
    [SerializeField] private LayerMask obstaclelayer;

    [Header("Audio")]
    [SerializeField] private AudioClip[] bounceClips; //부딪힐 때 랜덤 재생할 오디오 클립
    [SerializeField] private float audioCooldownTime = 2.0f; //소리를 너무 자주 재생하지 않기 위한 쿨타임
    private float lastAudioPlayedTime; //마지막으로 소리가 재생된 시간 저장

    [Header("Effects")]
    [SerializeField] private ParticleSystem particleSystem;
    private float effectCooldown = 1.0f; //이펙트 재생간 최소 간격
    private float timeToNextEffect = -1.0f; //다음 이펙트를 재생할 수 있는 시간

    private Vector3 inputVector;
    private float currentSpeed = 0.0f;
    private CharacterController characterController;
    private AudioSource audioSource;
    private float initialYPosition;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        initialYPosition = transform.position.y;
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        lastAudioPlayedTime = -audioCooldownTime;
    }

    void Update()
    {
        HandleInput();
        Move(inputVector);
    }

    //키보드 입력을 감지
    private void HandleInput()
    {
        float xInput = 0.0f;
        float zInput = 0.0f;

        if (Input.GetKey(forawardKey)) zInput++;
        if (Input.GetKey(backWardKey)) zInput--;
        if (Input.GetKey(leftKey)) xInput--;
        if (Input.GetKey(rightKey)) xInput++;

        inputVector = new Vector3(xInput, 0.0f, zInput);
    }

    private void Move(Vector3 inputVector)
    {
        if (inputVector == Vector3.zero)
        {
            if (currentSpeed > 0)
            {
                currentSpeed -= deceleation * Time.deltaTime; //조금씩 속도를 줄이기
                currentSpeed = Mathf.Max(currentSpeed, 0); //0보다 작아지지 않게 보정
            }
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, moveSpeed, Time.deltaTime * acceleration);
        }

        //이동할 거리를 계산
        Vector3 movement = inputVector.normalized * currentSpeed * Time.deltaTime;

        //캐릭터 컨트롤러를 이용해 이동
        characterController.Move(movement);

        transform.position = new Vector3(transform.position.x, initialYPosition, transform.position.z);
    }

    //일정 시간마다 효과음 재생
    public void PlayRandomAudioClip()
    {
        //마지막 재생 이후 일정 시간이 지났을때만 실행
        if (Time.time > (audioCooldownTime + lastAudioPlayedTime))
        {
            //시간 저장
            lastAudioPlayedTime = Time.time;

            //여러 소리중에 하나를 랜덤으로 선택
            audioSource.clip = bounceClips[Random.Range(0, bounceClips.Length)];

            //재생
            audioSource.Play();
        }
    }

    public void playEffect()
    {
        if (Time.time < timeToNextEffect) return;

        if (particleSystem != null)
        {
            ParticleSystem ps = Instantiate(particleSystem, transform.position, Quaternion.identity);
            particleSystem.Stop();
            particleSystem.Play();

            timeToNextEffect = Time.time + effectCooldown;
        }
    }

    //콜백 메서드 : 캐릭터 컨트롤러 전용 충돌 이벤트 메서드
    //게임 오브젝트에 캐릭터 컨트롤러 컴포넌트가 있어야 함
    //CharacterController.Move 또는 simpleMove()를 호출해서 이동할 때 충돌이 감지되면 이 메서드가 자동으로 호출
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //hit.gameObject.layer == 8
        //(1 << 8)
        //00000001 -> 00000001 00000000

        //player(8), enemy(9)를 동시에 체크했다면 obstaclelayer.value (1<<8) | (1<<9)

        //특정 게임 오브젝트가 지정된 LayerMask에 포함되어 있는가?
        if ((obstaclelayer.value & (1 << hit.gameObject.layer)) > 0)
        {
            PlayRandomAudioClip();
            playEffect();
        }

        //if (LayerMaskUtils.IsInLayer(hit.gameObject, obstaclelayer))
        //{
        //    PlayRandomAudioClip();
        //    playEffect();
        //}
    }
}
