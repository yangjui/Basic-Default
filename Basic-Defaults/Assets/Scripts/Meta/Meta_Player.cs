using UnityEngine;

[RequireComponent(typeof(CharacterController))] // ������Ʈ �ڵ� �߰�!

public class Meta_Player : Singleton<Meta_Player>
{
    [Header("Speed")]

    [SerializeField]
    private float normalSpeed = 5.0f;  // �⺻ ���ǵ�
    [SerializeField]
    private float walkSpeed = 5.0f;  // �ȱ�
    [SerializeField]
    private float runSpeed = 10.0f; // �޸���
    [SerializeField]
    private float jump = 10.0f; // ����
    [SerializeField]
    private float gravity = 20.0f;

    [Space(10f)]

    [Header("Warp")]

    [SerializeField]
    Transform warpTarget1 = null;
    [SerializeField]
    Transform warpTarget2 = null;

    private CharacterController Player = null; // ���� ĳ���Ͱ� �������ִ� ĳ���� ��Ʈ�ѷ�
    private Vector3 MoveDir = Vector3.zero; // ĳ������ �����̴� ����.
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
        Cursor.lockState = CursorLockMode.Locked; // ���콺 ��ġ ���� �� Ŀ�� ��Ȱ��ȭ
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
            // ��, �Ʒ� ������ ����. 
            MoveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

            // ���͸� ���� ��ǥ�� ���ؿ��� ���� ��ǥ�� �������� ��ȯ
            MoveDir = transform.TransformDirection(MoveDir.normalized);

            if (Input.GetKey(KeyCode.LeftShift)) // ����ƮŰ �޸���
            {
                normalSpeed = runSpeed;
            }

            else
            {
                normalSpeed = walkSpeed;
            }

            MoveDir *= normalSpeed;

            // ĳ���� ����
            if (Input.GetButton("Jump"))
            {
                MoveDir.y = jump;
            }

        }
        MoveDir.y -= gravity * Time.deltaTime;
        // ĳ���� ������.
        Player.Move(MoveDir * Time.deltaTime);
    }

    void PlayerRotate()
    {
        mouseX += Input.GetAxis("Mouse X") * mouseXSpeed;
        transform.eulerAngles = new Vector3(0, mouseX, 0);
    }

    void OnTriggerEnter(Collider _col)  // Ʈ���ſ� �浹�� �Ǿ��� ���� �� �Լ��� �����Ѵ�.
    {
        if (isWarp == false && _col.gameObject.CompareTag("Warp1"))
        {
            WarpChange();
            Player.enabled = false; //characterController�� ��� ���ΰ�
            Player.transform.position = warpTarget2.position; //���ϴ� ��ġ B�� �̵���Ű��       
            Player.enabled = true; //characterController�� �ٽ� �ѱ�

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

    void AnimationMoving() // �ִϸ��̼� ȿ�� - ���� �� ���� - ���� ���߿� �ٲ߽ô�
    {
        if (Input.GetKey("w") || Input.GetKey("s"))
        {
            anim.SetInteger("IsWalkingMotion", 1);
        }

        // ���� �ִϸ��̼� ����
        // ��ư ������ �Ķ���Ͱ� ���������� 2�� ��
        // �ٽ� else�� -> �Ķ���� 0 : ����, �������� ����
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