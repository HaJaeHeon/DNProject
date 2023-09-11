using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_PlayerCtrl : MonoBehaviour
{
    private Transform tr;
    private Rigidbody rbody;
    private Animator ani;
    CapsuleCollider col;
    //public GameObject target;
    H_PlayerRay pRay;
    H_PlayerGrab pGrab;


    public float moveSpeed = 2f;
    public float rotSpeed = 3f;
    public float jumpPower = 3f;
    public float h = 0f, v = 0f;

    bool jBD;
    public bool isjump;
    public bool isMove;
    public bool isCrawl;
    bool isRun;

    public Vector3 moveVec;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rbody = GetComponent<Rigidbody>();
        ani = GetComponent<Animator>();
        col = GetComponent<CapsuleCollider>();
        pRay = GetComponent<H_PlayerRay>();
        pGrab = GetComponent<H_PlayerGrab>();

        Vector3 moveVec = new Vector3(h, 0, v).normalized;

    }


    void Update()
    {
        GetInput();
        if (pGrab.isPull == false)
        {
            Move(1f);
        }
        else if (pGrab.isPull == true)
        {
            Move(-1f);
        }

        Jumped();

        if (!pGrab.isGrab && !pGrab.isObject)
        {
            Move(1f);
            MoveRot(1f);
        }

    }
    void GetInput()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        jBD = Input.GetKeyDown(KeyCode.Space);
        isCrawl = Input.GetKey(KeyCode.LeftControl);
        isRun = Input.GetKey(KeyCode.LeftShift);

    }
    public void Move(float x)
    {
        moveVec = new Vector3(h * x, 0, v);
        tr.position += moveVec * moveSpeed * Time.deltaTime;
        bool isMove = moveVec.magnitude != 0;
            
        ani.SetFloat("moveSpeed", moveSpeed);



        if (isCrawl)
        {
            ani.SetBool("isCrawling", true);
            moveSpeed = isMove ? 2f : 0f;
            col.height = isMove ? 1.6f : 1.2f;
            col.direction = isMove ? 2 : 1;
            if (isMove)
            {
                col.center = new Vector3(0f, 0.5f, 0f);
            }
            else if (!isMove)
            {
                col.center = new Vector3(0f, 0.6f, 0f);
            }
        }
        else if (!isCrawl)
        {
            ani.SetBool("isCrawling", false);
            moveSpeed = isMove ? 2f : 0f;
            col.height = 2f;
            col.direction = 1;
            if (isMove)
            {
                col.center = new Vector3(0f, 1f, 0f);

                if (isjump)
                    return;
                if (!isjump)
                    Run();

            }
            else if (!isMove)
            {
                col.center = new Vector3(0f, 1f, 0f);
            }
        }


    }
    void Run()
    {
        if (isRun)
            moveSpeed = 3f;
        if (!isRun)
            moveSpeed = 2f;

    }

    public void MoveRot(float x)
    {
        if (h == 0 && v == 0)
            return;
        moveVec = new Vector3(h * x, 0, v);
        Quaternion rot = Quaternion.LookRotation(moveVec);
        tr.rotation = Quaternion.Slerp(tr.rotation, rot, rotSpeed * Time.deltaTime);
    }

    void Jumped()
    {
        Vector3 moveVec = new Vector3(h, 0, v).normalized;
        Vector3 dir = (moveVec * 0.4f) + Vector3.up;
        if (jBD && !isjump)
        {
            if (pRay.isFloor)
            {
                if (isRun)
                {
                    isjump = true;
                    ani.SetTrigger("isJump");
                    ani.SetBool("isGround", false);
                    rbody.AddForce(dir * jumpPower, ForceMode.Impulse);
                }
                if (!isRun)
                {
                    isjump = true;
                    ani.SetTrigger("isJump");
                    ani.SetBool("isGround", false);
                    rbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
                }
            }

        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            ani.SetBool("isGround", true);

            isjump = false;
            pRay.isFloor = true;
        }
    }
}
