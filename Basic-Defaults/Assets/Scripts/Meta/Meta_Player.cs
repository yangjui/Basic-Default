using UnityEngine;

[RequireComponent(typeof(CharacterController))] // 컴포넌트 자동 추가!

public class Meta_Player : Singleton<Meta_Player>
{
    [Header("Speed")]

    [SerializeField]
    private float normalSpeed = 5.0f;  // 기본 스피드
    [SerializeField]
    private float walkSpeed = 5.0f;  // 걷기
    [SerializeField]
    private float runSpeed = 10.0f; // 달리기
    [SerializeField]
    private float jump = 10.0f; // 점프
    [SerializeField]
    private float gravity = 20.0f;

    [Space(10f)]

    [Header("Warp")]

    [SerializeField]
    Transform warpTarget1 = null;
    [SerializeField]
    Transform warpTarget2 = null;

    private CharacterController Player = null; // 현재 캐릭터가 가지고있는 캐릭터 컨트롤러
    private Vector3 MoveDir = Vector3.zero; // 캐릭터의 움직이는 방향.
    private Animator anim;
    private float mouseX = 0.0f;
    private float mouseXSpeed = 3.0f;
    private bool isWarp = false;


    void Awake()
    {
        Player = GetComponent<CharacterController>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 위치 고정 및 커서 비활성화
        Cursor.visible = false;
    }

    void Update()
    {
        PlayerMove();
        PlayerRotate();
    }

    void PlayerMove()
    {
        // if (Player.Position.y < 1.1)
        {
            // 위, 아래 움직임 셋팅. 
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // 벡터를 로컬 좌표계 기준에서 월드 좌표계 기준으로 변환
            MoveDir = transform.TransformDirection(MoveDir.normalized);

            if (Input.GetKey(KeyCode.LeftShift)) // 시프트키 달리기
            {
                normalSpeed = runSpeed;
            }

            else
            {
                normalSpeed = walkSpeed;
            }

            MoveDir *= normalSpeed;

            // 캐릭터 점프
            if (Input.GetButton("Jump"))
            {
                MoveDir.y = jump;
            }

        }
        MoveDir.y -= gravity * Time.deltaTime;
        // 캐릭터 움직임.
        Player.Move(MoveDir * Time.deltaTime);
    }

    void PlayerRotate()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseXSpeed;
        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    void OnTriggerEnter(Collider _col)  // 트리거에 충돌이 되었을 때는 이 함수를 도출한다.
    {
        if (isWarp == false && _col.gameObject.CompareTag("Warp1"))
        {
            WarpChange();
            Player.enabled = false; //characterController을 잠깐 꺼두고
            Player.transform.position = warpTarget2.position; //원하는 위치 B로 이동시키기       
            Player.enabled = true; //characterController을 다시 켜기

            Invoke("WarpChange", 2.0f);
        }

        else if (isWarp == false && _col.gameObject.CompareTag("Warp2"))
        {
            WarpChange();
            Player.enabled = false;
            Player.transform.position = warpTarget1.position;      
            Player.enabled = true;
            
            Invoke("WarpChange", 2.0f);
        }
    }

    private void WarpChange()
    {
        if (isWarp == true)
        {
            isWarp = false;
        }

        else
        {
            isWarp = true;
        }
    }

    void AnimationMoving() // 애니메이션 효과 - 현재 미 구현 - 변수 나중에 바꿉시당
    {
        if (Input.GetKey("w") || Input.GetKey("s"))
        {
            anim.SetInteger("IsWalkingMotion", 1);
        }

        // 점프 애니메이션 시작
        // 버튼 눌릴때 파라미터가 순간적으로 2가 됨
        // 다시 else문 -> 파라미터 0 : 공중, 착지까지 연속
        else if (Input.GetButtonDown("Jump"))
        {
            anim.SetInteger("JumpUpMotion", 2);
        }

        else
        {
            anim.SetInteger("JumpDownMotion", 0);
        }
    }
}