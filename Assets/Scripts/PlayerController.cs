using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    private Vector3 dir;
    public float speed;

    private Vector3 camDir;
    private Camera cam;
    private float camX;
    public float minXRot;
    public float maxXRot;
    public float sensitivity=0.1f;

    public float jumpPower;
    private LayerMask groundLayer;

    public bool canLook;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam =Camera.main;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        Move(); //Rigidbody 물리연산은 FixedUpdate
    }

    private void LateUpdate()
    {
        if (canLook)
        {
            Look(); //카메라는 모든 동작이 끝나고 나서 수행되어야 부자연스럽지 않기에 Late Update
        }
    }

    private void Move()
    {
        Vector3 moveDir = new Vector3(dir.x*speed, rb.velocity.y, dir.y*speed);
        rb.velocity = moveDir;
    }

    private void Look()
    {
        camX -= camDir.y*sensitivity;
        camX = Mathf.Clamp(camX, minXRot, maxXRot);

        cam.transform.localEulerAngles = new Vector3(camX,0,0); //위아래
        transform.localEulerAngles += new Vector3(0,camDir.x,0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed)
        {
            dir = context.ReadValue<Vector2>();
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        camDir = context.ReadValue<Vector2>(); // X축을 회전 시키면 위아래 Y축을 회전시키면 좌우가 돌아감 
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && IsGrounded())
        {
            rb.AddForce(Vector3.up,ForceMode.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position +(transform.forward*jumpPower)+(transform.up*jumpPower),Vector3.down),
            new Ray(transform.position +(transform.forward*jumpPower)+(-transform.up*jumpPower),Vector3.down),
            new Ray(transform.position +(-transform.forward * jumpPower)+(transform.up * jumpPower),Vector3.down),
            new Ray(transform.position +(-transform.forward * jumpPower)+(-transform.up * jumpPower),Vector3.down)
        };//대상위치에서 살짝 앞과 위 방향은 아래 

        for(int i = 0; i < rays.Length; i++)
        {
            if (Physics.Raycast(rays[i], 0.2f, groundLayer)) //Ray가 지면레이어와 닿았을시 true를 반환하여 점프가 가능하도록 한다.
                return true;
        }
        return false;
    }
}